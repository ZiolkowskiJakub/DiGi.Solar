# DiGi.Solar#8 — Investigation Results & Implementation Plan

> Status: **Still valid, but narrowed.** Both `null` → "fully sunlit" mappings named in the issue are still in the tree (verified 2026-09-24 on branch `0.8.8` @ `6ca3f95`, working tree clean). The concrete symptom the issue was observed through (a dropped shadow in the 720-surface benchmark row) **no longer reproduces** — [DiGi.Geometry#8](https://github.com/ZiolkowskiJakub/DiGi.Geometry/issues/8) landed and the benchmark now reports 0 dropped shadows (§2.2). What is left is exactly the issue's remaining scope: make the residual `null` path safe rather than silent, revert the test tolerance, and drop the tolerance to 0 (which is already green today).

---

## 1. Verdict

| Issue action item | Verdict |
|---|---|
| Fix 1a · Fall back to unmerged shadows clipped to the receiver, capped at the face area | **Valid and recommended.** One shared `Query` member + two call sites (§3 Phase 1/2). |
| Fix 1b · Or emit no result for the sample | **Valid only as the residual case** — kept for the one input shape where clipping is impossible (no receiver face). Documented in both `Solve()` `<summary>`s. |
| Fix 2 · "With DiGi.Geometry#8 the `null` path becomes rare" | **True and measured.** `Union` now retries with snap-rounding; `null` only after the second failure (`DiGi.Geometry/Planar/Query/Union.cs:163-174`). |
| Test · "the dropped count becomes 0 and `ShadingSolverBenchmarkDroppedFraction` goes back to 0" | **Already satisfied by the current binaries** — measured 0 dropped over 6 912 samples (§2.2). Test-constant change only; it does not depend on this fix. |
| Test · "A fact that forces a failing union (the WKT fixture from DiGi.Geometry#8 …)" | **Invalid as written.** That fixture now unions to 112.402 m², so it cannot force `null`. Replaced by a forced-failure stub + direct facts on the new member (§3 Phase 0/3, §2.3). |
| Acceptance · 0 warnings | **Valid.** `dotnet build DiGi.Solar.slnx` must stay warning-free; API markdown regenerates on build and must be committed. |
| Labels · `ai: light` | **No longer accurate.** Core shading logic, two solvers, one new `Query` member + facts → `ai: standard` per *GitHub - AI Issue Classification* §3 ("one `Query` … member and its `[Fact]`" = `standard`; "err higher for core business logic"). |

**Not blocked:** `gh api repos/ZiolkowskiJakub/DiGi.Solar/issues/8/dependencies/blocked_by` → `ZiolkowskiJakub/DiGi.Geometry#8 closed`.

---

## 2. Verification against the code (issue premises checked)

### 2.1 Both `null` → 0 mappings are still there — confirmed

- **CPU** — [DiGi.Solar/Classes/ShadingSolver.cs:338-355](DiGi.Solar/Classes/ShadingSolver.cs:338): `polygonalFace2Ds_Union = polygonalFace2Ds_Shadow.Union(); if (polygonalFace2Ds_Union != null) { … }`. On `null`, `polygonalFace2Ds_Result` stays empty and the receiver still gets a result with area 0 → `TryGetShadingFactor` returns `true` with `factor = 0`.
- **ComputeSharp** — [DiGi.Solar.ComputeSharp/Classes/ShadingSolver.cs:309](DiGi.Solar.ComputeSharp/Classes/ShadingSolver.cs:309) `polygonalFace2Ds = polygonalFace2Ds_Shadow.Union();` and line 312 `polygonalFace2Ds ??= [];` → same 0-area result.
- `TryGetShadingFactor` divides by the receiver area ([DiGi.Solar/Classes/ShadingModel.cs:171](DiGi.Solar/Classes/ShadingModel.cs:171)), and `GeometricalShadingSolverResult.Area` / `Create.ShadingSolverResult` both **sum face areas** — so the merged union is the only thing that stops double-counting. Any fallback must therefore cap, or a factor can exceed 1 (a second silent error, in the other direction).
- `Union` reaching `null` requires the snap-rounding `OverlayNG` retry to fail as well ([Union.cs:163-174](../DiGi.Geometry/DiGi.Geometry/Planar/Query/Union.cs)); a non-null `IEnumerable<IPolygonalFace2D>` never returns `null` for any other reason.

### 2.2 New finding — the observed symptom is gone; the tolerance can go to 0 today

Measured on this machine with the current tree (`DiGi.Geometry` `0.8.9` @ `dcdf582`, built 15:42):

```
dotnet test "DiGi.Solar.xUnit/DiGi.Solar.xUnit.csproj" -c Debug -m:1 \
  --filter "FullyQualifiedName~ShadingSolver_Benchmark_Receivers" --logger "console;verbosity=detailed"
→ Passed [44 s]
  Compared 6912 sun-facing factors across 720 receivers (4608 back-facing samples not compared, 0 dropped shadows); largest other difference 2.0473365114348496E-08
  144 | 720 | 720 | 0 | 1 | 1910.0 | 5768.7 | 3.02 | 0
```

`ShadingSolver_Benchmark_Surroundings`, `ShadingSolver_Solve_MatchesComputeSharp` and `ShadingSolver_ComputeDeviceType` likewise report `0 dropped shadows` throughout (24 s, all pass). The report file from earlier the same day (`user files/reports/ShadingSolver_Benchmark_Receivers.txt`, 15:49) still recorded `1` dropped at the 720-surface row — the run predates the 0.8.9 Geometry build. `ShadingSolverBenchmarkDroppedFraction = 0.001` is therefore already over-tolerance; setting it to `0` passes on unmodified DiGi.Solar.

Consequence for the plan: acceptance criterion 2 is **not** evidence that this fix works. The fix's own evidence must come from Phase 0/3.

### 2.3 New finding — the issue's proposed test fixture cannot force the failure

`files/Union_TopologyException.wkt` (the 190 shadow polygons of that east wall, committed with DiGi.Geometry#8) is the input the snap-rounding fallback now repairs. Placing it over a receiver exercises the *fallback inside* `Union`, not the solver-side `null` branch. After DiGi.Geometry#8 there is no input in the suite that drives `Union` to `null`, and building a fixture that defeats both `UnaryUnionOp` and `UnaryUnionNG` (snap grid `Tolerance.Distance` = 1e-6, `Union.cs:169`) would be machine- and NTS-version-dependent — a fixture like that fails open the day NTS changes. So the forced-failure path must be *injected*, not found (Phase 0), plus direct facts on the new member (Phase 3), per *Coding - Automatic Tests* §4 ("A Guard Must Be Shown To Fail").

### 2.4 New finding — the two solvers differ in whether shadows are clipped to the receiver

The CPU solver intersects every merged piece with the receiver face (`ShadingSolver.cs:345-351`); the ComputeSharp solver does not clip at all — its shadow faces are intersections with the receiver's own triangles, so they are inside the receiver by construction. The fallback must clip in both (a clipped, capped set is the only way to guarantee `factor ≤ 1`), and the cap must be the receiver **face** area, which is the divisor in `TryGetShadingFactor` (`polygonalFace3D.GetArea()`; equal to the planar `Geometry2D` area for a planar face). ComputeSharp currently has no receiver `PolygonalFace2D` in scope — it captures `planes_ShadingElements` in lockstep ([DiGi.Solar.ComputeSharp/Classes/ShadingSolver.cs:83-86](DiGi.Solar.ComputeSharp/Classes/ShadingSolver.cs:83), added at line 120); the fallback needs one more lockstep list.

### 2.5 Reachability — do not claim the fallback is exercised

No current fact reaches `Union` → `null` (§2.3), so the new branch is a safety net. Its coverage is the direct facts on the new `Query` member; its wiring into both solvers is proven by the injected-failure run (Phase 0), which must be reported and then removed.

---

## 3. Implementation plan

### Phase 0 — Reproduce before fixing (*Coding - Automatic Tests* §4, "Reproduce Before Fixing")

1. Add the permanent fact to `DiGi.Test/DiGi.Solar.xUnit/Facts/ShadingSolver.cs` (file already holds `ShadingSolver_Solve` and the `CreateShadingSolver` / `IsShadingSolverSupported` helpers — reuse them, and the `CreateVerticalWall` / `CreatePerformanceShadingModel` / `CreateDaytimeSeries` fixtures from `ShadingSolverPerformance.cs`):

```csharp
/// <summary>
/// Verifies on either solver that a partly shaded receiver never reads fully sunlit (factor above 0) and never reads over-covered (factor at or below 1).
/// <para>Two vertical walls (x = 6 and x = -2) throw overlapping shadows onto a 4 x 4 m receiver, the shape the shadow union exists to collapse.</para>
/// </summary>
[Theory]
[InlineData(false)]
[InlineData(true)]
[SupportedOSPlatform("windows")]
public void ShadingSolver_Solve_ShadowedReceiverNeverReadsSunlit(bool computeSharp)
```

   Assertions per sun-facing sample: `Assert.True(factor > 0.0, …)` for the known-shadowed receivers, and `Assert.True(factor <= 1.0, …)` for every sample (this second half is the permanent guard against double-counted shadows).

2. **Make it red without touching the shipped logic**: locally (never committed) replace the union call with a forced failure in both solvers — `List<PolygonalFace2D>? polygonalFace2Ds_Union = null;` where line 342 / 309 is today — run the fact, and record that it fails with `factor == 0` on both engines. That failure is the committed reproduction evidence (quote it in the PR/issue comment).
3. With the same stub, run the whole `DiGi.Solar.xUnit` suite and write the damage report to `Core.xUnit.Query.ReportsDirectory(...)`: number of samples that read 0, and the largest Σ-of-shadow-areas overshoot over the receiver area. The overshoot is the measurement that justifies the cap (§2.1) instead of asserting it.
4. Reachability probe (un-stubbed build, one temporary counter line in each solver, suite green, output to the reports directory): expected **0** real `null` unions in the whole suite — consistent with §2.5; report it, remove the counters.

### Phase 1 — One fallback implementation: `DiGi.Solar.Query.ShadowFaces`

New file `DiGi.Solar/Query/ShadowFaces.cs` (one member per file, named after the method; `Query`, not `Create`, and not duplicated as two private helpers in two solvers — *Coding - General* §2 "Method Encapsulation").

```csharp
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using System.Collections.Generic;

namespace DiGi.Solar
{
    public static partial class Query
    {
        /// <summary>
        /// Builds the shading of a receiver from unmerged shadow faces: each face is clipped to the receiver, and faces are kept in input order until their total area reaches the receiver face area.
        /// <para>Use when the merged union of the shadows is unavailable (<see cref="DiGi.Geometry.Planar.Query.Union(IEnumerable{IPolygonalFace2D}, double)"/> returned null). Overlapping shadows are counted twice up to the cap, which overstates the shaded area but never above the receiver, so a failed merge reads as shade rather than as full sun.</para>
        /// <para>Returns null when there is no receiver face to clip and cap against: the caller then has no defensible area to report and should emit no result for the sample.</para>
        /// </summary>
        /// <param name="polygonalFace2D_Receiver">The receiver face in its own plane coordinates, or null.</param>
        /// <param name="polygonalFace2Ds_Shadow">The shadow faces, in the receiver's plane coordinates, as produced by the solver before merging.</param>
        /// <returns>A list of shadow faces clipped to the receiver, whose summed area never exceeds the receiver face area; or null when <paramref name="polygonalFace2D_Receiver"/> is null.</returns>
        public static List<PolygonalFace2D>? ShadowFaces(this PolygonalFace2D? polygonalFace2D_Receiver, IEnumerable<PolygonalFace2D> polygonalFace2Ds_Shadow)
        {
            List<PolygonalFace2D> polygonalFace2Ds_Result = [];
            if (polygonalFace2D_Receiver == null)
            {
                return null;
            }

            double area_Cap = polygonalFace2D_Receiver.GetArea();
            if (double.IsNaN(area_Cap) || area_Cap <= 0)
            {
                return polygonalFace2Ds_Result;
            }

            double area_Accumulated = 0;
            foreach (PolygonalFace2D? polygonalFace2D_Shadow in polygonalFace2Ds_Shadow)
            {
                if (polygonalFace2D_Shadow == null)
                {
                    continue;
                }

                // Intersection returns null when the overlay itself failed; that face is then unknown, not empty, and is skipped rather than guessed at.
                List<PolygonalFace2D>? polygonalFace2Ds_Clip = polygonalFace2D_Shadow.Intersection(polygonalFace2D_Receiver);
                if (polygonalFace2Ds_Clip == null)
                {
                    continue;
                }

                foreach (PolygonalFace2D polygonalFace2D_Clip in polygonalFace2Ds_Clip)
                {
                    double area_Clip = polygonalFace2D_Clip.GetArea();
                    if (double.IsNaN(area_Clip) || area_Clip <= 0)
                    {
                        continue;
                    }

                    // The union is gone, so the receiver area is the only bound left: stop before it is broken, but always report the first face, however large, so a shaded receiver is never read as sunlit.
                    if (area_Accumulated > 0 && area_Accumulated + area_Clip > area_Cap)
                    {
                        return polygonalFace2Ds_Result;
                    }

                    area_Accumulated += area_Clip;
                    polygonalFace2Ds_Result.Add(polygonalFace2D_Clip);
                }
            }

            return polygonalFace2Ds_Result;
        }
    }
}
```

Properties worth stating in review: for **disjoint** shadows (the common case) the result is exactly the union, so the fallback is exact, not merely bounded; for overlapping shadows the reported area is in `[max face, receiver area]`; deterministic (input order, no sorting).

**Why the parameter is `IEnumerable<PolygonalFace2D>`, not `IEnumerable<IPolygonalFace2D>`:** `Intersection` exists only for the concrete pair (`DiGi.Geometry/Planar/Query/Intersection.cs:18`), so an interface-typed loop variable cannot clip. That forces one line of the ComputeSharp call site to change type (§Phase 2), and the parameter stays non-nullable because the contract requires a list — the only `null` the member can return is "no receiver face".

The CPU solver's `List<PolygonalFace2D> polygonalFace2Ds_Shadow` (line 210) and the new helper therefore agree on the concrete type, matching `Create.PolygonalFace2D`, which returns `PolygonalFace2D?` (`DiGi.Geometry/Planar/Create/PolygonalFace2D.cs:16`, `:116`).

### Phase 2 — Wire both solvers, and document the choice

**CPU** — [DiGi.Solar/Classes/ShadingSolver.cs:340-355](DiGi.Solar/Classes/ShadingSolver.cs:340): add the `else` to `if (polygonalFace2Ds_Union != null)`

```csharp
                        else
                        {
                            // The merge failed (it stays possible even after the snap-rounding retry DiGi.Geometry makes): keep the unmerged shadows clipped to the receiver and capped at its area, so a failed merge reads as shade, overstated at worst, and never as full sun. The receiver face is never null here, so the fallback is the shadow set or an empty one.
                            if (Query.ShadowFaces(polygonalFace2D, polygonalFace2Ds_Shadow) is List<PolygonalFace2D> polygonalFace2Ds_Fallback)
                            {
                                polygonalFace2Ds_Result.AddRange(polygonalFace2Ds_Fallback);
                            }
                        }
```

The `null` branch of `ShadowFaces` is unreachable here: line 175 already returns for a receiver without a face.

**ComputeSharp** — [DiGi.Solar.ComputeSharp/Classes/ShadingSolver.cs:305-312](DiGi.Solar.ComputeSharp/Classes/ShadingSolver.cs:305): replace the single assignment with

```csharp
                        List<PolygonalFace2D>? polygonalFace2Ds_Union = polygonalFace2Ds_Shadow.Union();
                        polygonalFace2Ds = polygonalFace2Ds_Union ?? Solar.Query.ShadowFaces(polygonalFace2D_Receiver, polygonalFace2Ds_Shadow);
```

Plus two companion changes in the same method:

1. **Widen the shadow list** at lines 291 and 299 (`List<IPolygonalFace2D>` / `is IPolygonalFace2D polygonalFace2D_Shadow`) to `List<PolygonalFace2D>` / `is PolygonalFace2D polygonalFace2D_Shadow`, so it matches the helper and the CPU solver. `polygonalFace2Ds_Shadow.Union()` keeps binding to `Union(IEnumerable<IPolygonalFace2D>)` — the overload that returns `List<PolygonalFace2D>` and preserves holes — because `PolygonalFace2D : Geometry2D, IPolygonalFace2D` (`DiGi.Geometry/Planar/Classes/PolygonalFace2D.cs:15`) and `IPolygonalFace2D : IFace2D<IPolygonal2D>`: it does **not** implement `IPolygonal2D`, so the generic `Union<TPolygonal2D>` overload, which would silently return `List<Polygon2D>` and drop holes (`Union.cs:220`), stays out of contention. Confirm this with the build output, not by reading, and say so in the commit message (*Coding - General* §1.9/§1.17).
2. **Capture the receiver face next to the planes**, extending the existing lockstep comment at lines 83-86 and the `planes_ShadingElements.Add(...)` at line 120:

```csharp
            List<PolygonalFace2D?> polygonalFace2Ds_ShadingElements = [];
            // …in the non-shading-only branch:
            polygonalFace2Ds_ShadingElements.Add(polygonalFace3D?.Geometry2D as PolygonalFace2D);
            // …inside Parallel.For, next to planes_ShadingElements[i]:
            PolygonalFace2D? polygonalFace2D_Receiver = polygonalFace2Ds_ShadingElements[i];
```

Keep `polygonalFace2Ds ??= [];` (line 312) — it is the "no shadow triangles at all" path, not this one.

**XML docs** — extend both `Solve()` `<summary>` blocks (they are the contract the issue asks to document), one `<para>` each, same wording in both:

> *<para>When the merged shadow union fails, the unmerged shadows are clipped to the receiver and used instead, capped at the receiver area: the shaded area of that sample is then overstated but never exceeds the receiver and never reads as full sun. If there is no receiver face to cap against, the sample gets no result and `TryGetShadingFactor` returns false for it.</para>*

Rebuild regenerates `documentation/API/DiGi.Solar/DiGi.Solar.md` (and the ComputeSharp page); commit it with the code.

### Phase 3 — Tests (`DiGi.Test/DiGi.Solar.xUnit`)

1. `Facts/ShadowFaces.cs` — direct facts on the new member (always-on coverage of the arithmetic, no GPU needed):
   - `ShadowFaces_DisjointShadows_ArePassedThroughAtTheirExactArea` — three non-touching faces inside the receiver ⇒ total area equals Σ of the inputs (fallback exactness on the common case).
   - `ShadowFaces_OverlappingShadows_AreCappedAtTheReceiverArea` — faces whose Σ is ~2× the receiver ⇒ total area > 0 and ≤ the receiver area. **Show the guard bites**: temporarily delete the cap check, re-run, and confirm this fact fails (it must not be able to pass on an un-capped implementation) — *Coding - Automatic Tests* §4, "A Guard Must Be Shown To Fail".
   - `ShadowFaces_FacesOutsideTheReceiver_AreClipped` — a face half off the receiver contributes only the part inside.
   - `ShadowFaces_NullReceiver_ReturnsNull` / `ShadowFaces_EmptyShadow_ReturnsEmpty`.
   - `ShadowFaces_CapEqualsSolverDivisor` — for a real `PolygonalFace3D` receiver, `polygonalFace3D.GetArea()` equals `polygonalFace3D.Geometry2D` area to 9 decimals, pinning the assumption in §2.4 that the cap is the divisor `TryGetShadingFactor` uses.
2. `Facts/ShadingSolver.cs` — `ShadingSolver_Solve_ShadowedReceiverNeverReadsSunlit` from Phase 0 (red under the stub, green after Phase 2, green stub-free).
3. `Facts/ShadingSolverBenchmark.cs` — set `ShadingSolverBenchmarkDroppedFraction` back to `0` and correct the now-stale prose: the helper `<para>` in `Facts/ShadingSolverCPU.cs` (lines ~160-172) still describes "one east wall of the 720-surface benchmark grid at a 3 degree sun" as a live ComputeSharp defect; it is not (it was DiGi.Geometry#8). Keep the `Dropped shadows` column and the counter — at tolerance 0 they are the guard. `ShadingSolver_Benchmark_Software` keeps its explicit `1.0` and its `Skip`.
4. Regression net: full `DiGi.Solar.xUnit` suite green; then the facts of issues #4 and #5 (`Facts/ShadingSolverCPU.cs`, `Facts/ShadingModel.cs`) especially.

### Phase 4 — Build & cross-repo verification

```powershell
dotnet build "DiGi.Solar.slnx" -m:1                                 # zero warnings, regenerates documentation/API
dotnet test "DiGi.Solar.xUnit\DiGi.Solar.xUnit.csproj" -c Debug -m:1
dotnet test "DiGi.Solar.xUnit\DiGi.Solar.xUnit.csproj" -c Release -m:1 --filter "FullyQualifiedName~ShadingSolver_Benchmark"   # benchmark measured isolated
```

- `DiGi.Solar.xUnit` reaches both solvers and `DiGi.Geometry` by **`ProjectReference`** (verified in the `.csproj`), so `dotnet test` rebuilds them; still build `DiGi.Solar` first per *Coding - Automatic Tests* §4 (other consumers reach it by `HintPath` and will not).
- Consumers to rebuild, not change: `DiGi.Solar.ComputeSharp` (same repo) and any host of `DiGi.Solar.dll` — no signature removed, so no compile impact; no new NuGet dependency, so *Coding - General* §4 does not apply.
- Commit on the active version branch `0.8.8` (DiGi.Solar) and `0.8.12` (DiGi.Test); patch bump happens through the normal branch-sync flow. Adding a public `Query` member is additive — no wire/`_type` change, so no migration.
- **Prerequisite:** this session has read-only access to `DiGi.Test`; write access is required for Phase 0/3.

### Acceptance criteria mapping

| Criterion (issue #8) | Where it lands | Status today |
|---|---|---|
| Neither solver reports factor 0 for a receiver whose shadow union failed | Phase 1 + 2, proven by Phase 0's stubbed run and the Phase 3 facts | open |
| `ShadingSolver_Benchmark_Receivers` passes with `ShadingSolverBenchmarkDroppedFraction = 0` | Phase 3.3 | **meets it already** (§2.2) — a test-only revert |
| 0 warnings | Phase 4 | open |

---

## 4. Explicit rejections / out of scope

1. **Emitting no result as the primary remedy** (issue's option 1b) — rejected as the general answer: it breaks the "one result per daytime timestamp" contract from issue #4, `TryGetShadingFactor` would interpolate across the gap, and the parity helper asserts `Assert.Equal(hasFactor_Other, hasFactor_CPU)`, so a one-engine gap reads as a disagreement. Kept only for the "no receiver face" residual.
2. **Aborting `Solve()` (return false) when a union fails** — rejected; one degenerate sample would discard an entire analysis run. The failure is sample-local and should stay sample-local.
3. **Catching `TopologyException` in the solvers** — rejected; robustness is owned by `DiGi.Geometry` (the snap-rounding retry, `Union.cs:163-174`). A second catcher in the consumers would drift.
4. **Clamping in `TryGetShadingFactor`** — rejected; it fixes the reading, not the cause, and would silently hide any future double-counting (the `factor ≤ 1` assertions in Phase 3 are what make that visible instead).
5. **Passing `ShadingSolverOptions.Tolerance` into `Union(...)`** — deferred, separate decision. Today both sides use `Core.Constants.Tolerance.Distance` (1e-6), so nothing changes; but `Union`'s parameter is a *snap-rounding grid size*, and letting a user-set solver tolerance become a geometry-snapping grid is a real behavioural question, not a rename.
6. **Not clipping the ComputeSharp merged union to the receiver face at all** — pre-existing, unrelated to this issue, and the reason a `factor > 1` result is conceivable on the success path. Measured? No. Needs its own issue with its own fixture; do not fold it in here.
7. **A test-only "force union failure" option on `ShadingSolverOptions`** — rejected; a production knob that exists only for tests is exactly the parallel source of truth the guidelines prohibit. The Phase 0 stub is throwaway and never committed.
8. **A fixture engineered to defeat both union passes** — rejected (§2.3): brittle across machines and NTS versions, and it fails open.

---

## 5. Behavioural change statement

Only for samples where the shadow merge fails — which no input in the current suite reaches (§2.5):

- **Before:** the receiver gets a zero-area result; `TryGetShadingFactor` returns `true` with `factor = 0`, i.e. full sun, and irradiance is overstated silently.
- **After (CPU and ComputeSharp):** the receiver gets the unmerged shadows clipped to its face, total area bounded by that face. `factor` is strictly positive and ≤ 1; it overstates the shaded area by up to the double-counting the union would have removed (bounded by the receiver area). With the stubbed-failure run, quantify and report the actual overshoot on the two-wall fixture instead of asserting a bound.
- **New public API:** `DiGi.Solar.Query.ShadowFaces(this PolygonalFace2D?, IEnumerable<PolygonalFace2D>)` — additive; regenerated `documentation/API/DiGi.Solar/DiGi.Solar.md` must be committed in the same change.
- `ShadingSolverBenchmarkDroppedFraction` `0.001 → 0` tightens an existing assertion; the benchmark already measures 0.

---

## 6. Corrections to post on the issue (do not edit the body)

Post as one comment (`--body-file`, per *GitHub - Issues* §1):

```markdown
## Premise check before implementation (2026-09-24, `0.8.8` @ `6ca3f95`)

Both `null` → 0 mappings are still in place (`Classes/ShadingSolver.cs:338-355`, `DiGi.Solar.ComputeSharp/Classes/ShadingSolver.cs:309-312`), so the issue stays open. Two of its statements are now stale:

1. **The observed symptom no longer reproduces.** `ShadingSolver_Benchmark_Receivers` on the current tree (DiGi.Geometry `0.8.9` @ `dcdf582`): `Compared 6912 sun-facing factors across 720 receivers … 0 dropped shadows`, largest other difference `2.05E-08`, `Dropped shadows` column `0` on every row. `ShadingSolverBenchmarkDroppedFraction = 0` passes **without** any DiGi.Solar change — criterion 2 is a test-constant revert, not evidence of this fix.
2. **The proposed test cannot force the failure.** `files/Union_TopologyException.wkt` is the input the snap-rounding fallback now repairs; over a receiver it exercises DiGi.Geometry's retry, not the solver's `null` branch. No fixture should chase a double `TopologyException` (fails open across NTS versions). Substitute: (a) direct facts on the new fallback member, (b) a throwaway forced-`null` stub in both solvers proving `ShadingSolver_Solve_ShadowedReceiverNeverReadsSunlit` red-before/green-after — never committed.

Re-scoped scope: one `Query` member (clip-and-cap fallback), its two call sites, its `<summary>` in both `Solve()`s, the test facts, and the tolerance revert. Recommend relabelling `ai: light` → `ai: standard` (*GitHub - AI Issue Classification* §3).

Plan: `DiGi.Solar/issue-8-implementation-plan.md`.
```

Then: `gh issue edit 8 --repo ZiolkowskiJakub/DiGi.Solar --remove-label "ai: light" --add-label "ai: standard"` (open issue, so label updates are allowed per *GitHub - Issues* §1).

---

## 7. Implementation result (2026-09-24)

Shipped as specified, with three deltas noted at the end.

**Code (`DiGi.Solar`, branch `0.8.8`)**

- New `DiGi.Solar/Query/ShadowFaces.cs` — `public static List<PolygonalFace2D>? ShadowFaces(this PolygonalFace2D? polygonalFace2D_Receiver, IEnumerable<PolygonalFace2D> polygonalFace2Ds_Shadow)`.
- CPU solver: `else` branch on the union check; ComputeSharp solver: `polygonalFace2Ds_Union ?? Solar.Query.ShadowFaces(polygonalFace2D_Receiver, polygonalFace2Ds_Shadow)`, new lockstep `polygonalFace2Ds_ShadingElements`, shadow list widened to `List<PolygonalFace2D>` (the `Union()` call still binds to the `IEnumerable<IPolygonalFace2D>` overload — confirmed by the build, not by reading), now-unused `using DiGi.Geometry.Planar.Interfaces;` removed.
- Fallback paragraph added to both `Solve()` `<summary>`s; `documentation/API/` regenerated and committed with it.

**Tests (`DiGi.Test`, branch `0.8.12`)** — `Facts/ShadowFaces.cs` (six facts), `Facts/ShadingSolver.cs` (`ShadingSolver_Solve_ShadowedReceiverNeverReadsSunlit`, Theory over both solvers), `ShadingSolverBenchmarkDroppedFraction` `0.001 → 0`, stale dropped-shadow prose in `Facts/ShadingSolverCPU.cs` corrected.

| Evidence | Result |
|---|---|
| A · red before the fix (union forced to `null` in **both** solvers) | All 16 sun-facing samples of the two-wall fixture read `0.0000` on both engines (against `0.8039 … 1.0000` unstubbed); `ShadingSolver_Solve_ShadowedReceiverNeverReadsSunlit` failed. |
| B · after the fix, no stub | Same fact passes: 15 of 16 sun-facing samples shaded on both engines, every factor inside `[0, 1]`. |
| C · wiring (union forced to `null` in the **ComputeSharp solver only**) | The new fact still passes (15 of 16 on both, nothing outside `[0, 1]`), while the parity facts fail with `differ by up to 0.49999999999999967` — i.e. the degraded path keeps shade as shade and overstates it by up to half the receiver, which is the double-counting the merge would have removed. |
| Guard shown to bite (cap check deleted from `ShadowFaces`) | `ShadowFaces_OverlappingShadows_AreCappedAtTheReceiverArea` fails: `Overlapping shadows sum to 150, over the 100 m2 receiver`. |
| Suite | `DiGi.Solar.xUnit` Debug: **70 passed, 0 failed, 1 skipped** (the WARP benchmark), **0 warnings**, 2 m 01 s. |
| Benchmarks isolated, Release, `ShadingSolverBenchmarkDroppedFraction = 0` | Both pass. Largest row of the receivers sweep: 144 buildings / 720 surfaces / 1930.4 ms CPU / 6006.9 ms hardware / ratio 3.11 / **0 dropped shadows**. |

**Deltas from the plan**

1. The solver-level fact compares *shaded sample counts* between engines and bounds every factor to `[0, 1]`, rather than asserting `factor > 0` per sample: this fixture has a legitimately sunlit sample (local noon, both walls project past the receiver), measured 15 shaded of 16 sun-facing, and the fixture guard is `>= 12`.
2. The cap keeps the first clipped face unconditionally (`area_Accumulated > 0 &&` on the stop test), so a shaded receiver cannot read sunlit even if a single clipped face overshoots the receiver area.
3. `ShadowFaces` is called in static form (`Query.ShadowFaces(receiver, shadows)`) rather than as `receiver.ShadowFaces(shadows)`, to keep the two call sites unambiguous about which `Query` they mean.
