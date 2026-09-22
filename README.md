# Fruit Merge

Fruit Merge is a fruit-themed merge puzzle game built in Unity. Slide fruits across a grid, merge matching pairs into the next fruit in the chain, and work through hand-crafted levels to reach each level's target fruit before the board fills up.

<p align="center">
  <img src="docs/banner.png" alt="Fruit Merge banner" width="720">
</p>

## Overview

The core loop is simple to learn and hard to put down: two matching fruits combine into the next fruit in an 11-step chain, from Cherry all the way up to Watermelon. Each level defines a specific target fruit to reach at least once; running out of space on the grid before reaching it ends the run. Progress and scores are tracked per level, and levels unlock sequentially as they are completed.

The game is designed to be played comfortably on both desktop and mobile: keyboard controls on desktop, swipe/drag gestures on touch devices, with no on-screen input hints required.

## Gameplay

- **Grid:** 4x4 board
- **Merge rule:** two fruits of the same type combine into the next fruit in the chain
- **Objective:** reach the level's target fruit at least once
- **Loss condition:** the grid fills up with no valid merges remaining
- **Scoring:** each merge awards points based on the resulting fruit's position in the chain; a per-level high score is tracked and flagged when beaten

## Fruit Chain

| Step | Fruit        | Points |
|------|--------------|--------|
| 1    | Cherry       | 5      |
| 2    | Strawberry   | 10     |
| 3    | Green Grape  | 20     |
| 4    | Purple Grape | 35     |
| 5    | Kiwi         | 55     |
| 6    | Pear         | 70     |
| 7    | Apple        | 90     |
| 8    | Banana       | 105    |
| 9    | Orange       | 140    |
| 10   | Pineapple    | 180    |
| 11   | Watermelon   | 230    |

## Levels

The game currently ships with 5 levels, each targeting a specific fruit in the chain with an increasing difficulty curve. Future updates are planned to expand the level count and introduce new objective types, such as reaching a target quantity of a fruit or completing a level within a limited number of moves.

## Controls

| Platform          | Input                          |
|-------------------|---------------------------------|
| Desktop            | WASD / arrow keys              |
| Mobile / touch     | Swipe gesture                  |
| Desktop (alternate)| Click-and-drag                 |

## Screens

- Main Menu (Play, Levels, Settings, Quit)
- Levels (locked/unlocked level selection)
- In-game HUD (score, high score, current level target)
- Level Completed
- Game Over
- Settings (music and sound effects toggles)

## Tech Stack

- **Engine:** Unity
- **Language:** C#
- **Data architecture:** ScriptableObject-based fruit and level definitions
- **Persistence:** local save data for level progress and high scores

## Project Status

Actively in development. The core merge mechanic, level system, and UI flow are being implemented on top of a grid/merge foundation, with visual assets, audio, and platform-specific controls layered in incrementally.

## Roadmap

- [ ] Core grid and merge logic
- [ ] Level and target-fruit system (5 levels)
- [ ] Score and high score tracking
- [ ] Full UI flow (menus, levels screen, popups, settings)
- [ ] Desktop and touch input support
- [ ] Audio and polish pass
- [ ] Quantity-based level objectives
- [ ] Move-limited level objectives
- [ ] Additional levels

## License

All rights reserved. Source code and assets in this repository may not be reused, redistributed, or repurposed without permission.

---

*The grid and merge logic build on a classic sliding-tile merge mechanic; the level system, scoring, fruit chain, art direction, and UI are original to this project.*
