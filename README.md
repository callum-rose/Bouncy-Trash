# Bouncy Trash

![Demo Gameplay](Screenshots/Demo%20Gameplay.jpeg)

A 2D arcade reflex game built in Unity. This repository is a vertical slice — a playable core loop used to prove out the architecture and the gameplay system (deterministic projectile scheduling).

## The game

The play space is a building described by a simple grid: 3 storeys tall and 3 lateral positions wide (see `GameDimensions`). Trash and coins spawn from the storeys and travel downward along parabolic bounce arcs, hopping from one position to the next.

The player controls a trampoline (or more generally a Bouncer) at the base of the building and moves it left/right between the three positions.

Each time a falling object reaches the end of an arc, the game resolves one of three outcomes:

- **Bounce** — the Bouncer is aligned under the object, so it bounces and continues to the next position.
- **Score** — the object is bounced all the way past the final position; the player scores.
- **Crash** — the Bouncer is *not* aligned, so the object hits the ground. Trash costs a life; coins are collected as currency.

The result is a fast lane-switching reflex game where the player reads the descending pattern and positions the Bouncer to keep trash in play while managing coins and lives. Projectile *kinds* (trash vs. coin) are chosen by a weighted random selector, and the spawn theme/skin is data-driven.

## Projectile Scheduling

The hardest design constraint in a game like this is fairness: the player must always physically be able to react. If two objects land on the floor in the same instant at two different positions, the Bouncer can't be in two places at once — the game would be unwinnable through no fault of the player.

`ProjectileSchedule` solves this with a frame-quantised ring-buffer scheduler (`Assets/Scripts/Game/Projectile/Scheduler/`):

- Time is discretised into fixed frames (`GameTimings.FrameDuration`). Bounces can only ever resolve on a frame boundary.
- Each storey owns a circular buffer of `Frame` slots (`FrameBuffer` over a `CircularBuffer<Frame>`), representing the upcoming bounce timeline. The whole timeline `Shift()`s forward one slot per frame.
- Because higher storeys produce slower, taller arcs (`GetProjectileDuration(storey)`), each storey's bounce cadence differs. Before committing a new projectile, the scheduler computes the exact frame indices it *would* occupy and runs a collision check across all storey buffers (`CanInsertIntoBuffer`) — it only spawns if every landing frame is either free or already claimed by the *same* position.
- Each scheduled projectile gets an id, so if it crashes early the scheduler can `Cancel(id)` and free every future slot it reserved.

## Architecture

The codebase leans on patterns that keep gameplay logic testable and content-driven rather than hard-coded.

**Data-driven content with ScriptableObjects.** Bouncers, projectile themes, and scenery are authored as `ScriptableObject` assets (each carrying a stable `Guid` via `NameAndIdScriptableObject`) and loaded generically through `AssetLoader<T>` over Unity's `Resources` system. Adding content means creating an asset, not editing code.

**Composable projectile paths (decorator pattern).** Movement paths are built from small `IPath` primitives — `BasicPath` (a parabola), `FlatPath`, `OffsetPath`, `HalfSplicedPath` — combined through fluent extension methods (`GetStartAboveOrigin()`, `GetFirstHalfFlattened()`, `GetFollowOnFrom()`). A `PathTraverser` walks a path over time, and consecutive arcs are stitched together so a projectile flows seamlessly across the grid.

**Interface-first, factory/builder construction.** Gameplay objects are created behind interfaces (`IBouncer`, `IProjectileFactory`, `IProjectileKindSelector`, `IProjectileSchedule`, `IBouncerMovementController`, `IPathFactory`, …) and assembled via dedicated factories/builders (`BouncerFactory` → `BouncerBuilder`, `ProjectileKindFactory`, `TrashFactory`/`CoinFactory`). Input is similarly abstracted — `KeyboardBouncerMovementController` is one swappable implementation of `IBouncerMovementController`, raising movement as events rather than mutating state directly.

**Additive multi-scene composition.** The game boots by additively loading three independent scenes — `Game`, `Scenery`, and `GameUI` — via `GameStarter`. Each scene exposes a `BaseSceneManager` that receives its dependencies through a typed `IPassThroughData` payload, giving a clean, inspector-free form of dependency injection across scene boundaries.

**Event-driven decoupling.** Score, lives, and coins live in a plain-C# `GameStats` that exposes `IGameStatsNotifier` / `IGameStatsUpdatable`. The movement layer raises `ProjectileBounced` / `ProjectileCompleted` / `ProjectileHitGround` / `ProjectileOffScreen` events that the `ProjectileManager` translates into stat changes and UI updates, keeping systems independent.

**Single source of truth for geometry.** `GameDimensions` centralises the entire coordinate system — grid size, storey height, position width, spawn points, and the destroy zone — so layout and gameplay math never drift apart.