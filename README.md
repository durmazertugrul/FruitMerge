# Fruit Merge

Fruit Merge is a fruit-themed merge puzzle game built in Unity. Slide fruits across a 4x4 grid, merge matching pairs into the next fruit in an eleven-step chain, and work through fifteen hand-tuned levels, each asking for a set number of a specific fruit before the board runs out of space.

<p align="center">
  <img src="docs/banner.png" alt="Fruit Merge banner" width="720">
</p>

## Overview

Two fruits of the same kind combine into the next fruit in the chain, from Cherry up to Watermelon. Every level defines a target fruit and how many of it the player has to produce; the run ends when the grid fills with no valid merges left. Scores and progress are stored per level, and levels unlock in sequence as they are completed.

What separates this project from a straightforward merge game is how its levels were built. Rather than setting objectives by feel, every candidate objective was measured in a simulation that replicates the game's own merge rules, and the shipped set was selected from those measurements. That work is documented in [Level design and balancing](#level-design-and-balancing).

<p align="center">
  <img src="docs/merge-example.png" alt="Two cherries merging into a strawberry" width="420">
</p>

## Gameplay

- **Grid:** 4x4
- **Merge rule:** two fruits of the same type combine into the next fruit in the chain
- **Spawn:** after every move a new fruit drops into a random empty cell, 90% Cherry and 10% Strawberry
- **Objective:** produce the required number of the level's target fruit
- **Loss condition:** the grid fills up with no valid merges remaining
- **Scoring:** every merge awards the point value of the fruit it produces; a high score is kept separately for each level

Only fruits produced by a merge count toward an objective. The Cherries that drop onto the board are not counted, which is what makes a low-tier objective such as "25 Green Grapes" a test of how long the player can keep the board alive.

## Fruit chain

<p align="center">
  <img src="docs/fruit-chain.png" alt="The eleven-step fruit chain with point values" width="760">
</p>

| Step | Fruit        | Points |
|------|--------------|--------|
| 1    | Cherry       | 5      |
| 2    | Strawberry   | 10     |
| 3    | Green Grape  | 20     |
| 4    | Purple Grape | 35     |
| 5    | Peach        | 55     |
| 6    | Pear         | 70     |
| 7    | Apple        | 90     |
| 8    | Banana       | 105    |
| 9    | Orange       | 140    |
| 10   | Pineapple    | 180    |
| 11   | Watermelon   | 230    |

## Levels

<p align="center">
  <img src="docs/levels.png" alt="The fifteen level objectives" width="860">
</p>

| Level | Target fruit | Required | Level | Target fruit | Required |
|-------|--------------|----------|-------|--------------|----------|
| 1     | Purple Grape | 1        | 9     | Peach        | 7        |
| 2     | Peach        | 2        | 10    | Apple        | 1        |
| 3     | Green Grape  | 25       | 11    | Pear         | 4        |
| 4     | Purple Grape | 6        | 12    | Purple Grape | 20       |
| 5     | Pear         | 2        | 13    | Peach        | 8        |
| 6     | Purple Grape | 13       | 14    | Apple        | 2        |
| 7     | Peach        | 4        | 15    | Pear         | 5        |
| 8     | Pear         | 3        |       |              |          |

Difficulty is deliberately shaped as a sawtooth rather than a straight climb: the set is grouped into five groups of three levels, and each group opens above the previous group's hardest level before cutting deeper than it. Within any one fruit, the required count only ever grows as the levels advance, so a player never meets an easier version of an objective they have already cleared.

## Level design and balancing

Setting objectives without measuring them produces errors in both directions, and the expensive one is an objective that cannot be cleared: the player reads it as the game being broken rather than as their own limit. Every objective in the shipped set was therefore measured before it was placed.

A simulation was written that applies the game's merge rules exactly, including the 4x4 board, the 90/10 spawn distribution and the rule that only merge results count toward an objective. Three player models of differing decision quality play it: one selecting at random, one greedy on merges and free space, and one weighing free space, board smoothness, merges and corner discipline together. Each model attempted each level 250 times.

Three rounds were run, across **38,100 simulated games**:

| Round | Design | Result |
|-------|--------|--------|
| 1 | Five levels, each asking to reach one fruit once | Eliminated. The strongest model reached Pineapple in 14% of games and Watermelon in none. |
| 2 | Fifteen levels with quantity objectives, ordered by intuition | Eliminated. Three levels sat below 2% for the mid-skill model, and the curve spiked at level nine before easing again. |
| 3 | Fifteen levels selected from a measured difficulty map | Adopted. The set below. |

For the third round, 69 candidate objectives were each measured 150 times to build a difficulty map, and the set was then selected under three constraints: every level must be clearable, the curve must advance as a sawtooth, and the required count for a given fruit may only increase over the sequence.

The adopted set was re-verified at 250 trials per level per model. On its hardest level, the high-skill model completes 98% of attempts, the mid-skill model 32%, and the model playing entirely at random 7%, so no level is unclearable even under random play.

One finding shaped the constraints as much as the numbers did. Producing one Banana requires producing two Apples first, so a "Banana x1" objective contains an "Apple x2" objective and is easier than "Apple x3" despite sitting higher in the chain. Raising a required count is a harsher difficulty lever than advancing one step up the chain, which is why the final ordering could not be derived from chain position alone.

The full report, including the per-level tables and difficulty curves, is in [`docs/level-test-report.pdf`](docs/level-test-report.pdf).

## Controls

| Platform | Input |
|----------|-------|
| Desktop  | WASD or arrow keys |
| Touch    | Swipe gesture (in development) |

## Screens

- Main Menu (Play, Levels, Settings, Quit)
- Levels (sixteen tiles, locked and unlocked states resolved at runtime)
- In-game HUD (score, high score, level number, target fruit and remaining count)
- Level Completed (Continue, Try Again, Main Menu)
- Game Over (Try Again, Main Menu, Quit)
- Settings (music and sound effect toggles)

## Architecture

- **Engine and language:** Unity, C#
- **Data:** fruit tiers and level definitions are ScriptableObjects, so objectives and point values are authored as assets rather than compiled into code
- **UI:** an event-driven HUD. The game manager raises score, objective-progress and level-outcome events, and the UI components subscribe to them; nothing polls game state per frame
- **Level flow:** level transitions reload the gameplay scene rather than resetting state by hand, which keeps animation, input and board state from leaking between attempts
- **Persistence:** local save data for per-level high scores and unlock progress
- **Audio:** a single persistent music source survives scene loads behind a duplicate guard; the merge sound's pitch is scaled by the tier of the fruit it produces, so climbing the chain is audible
- **Tooling:** the balancing simulation is a separate Python harness and is not part of the game build

## Project status

In active development. The merge mechanic, the objective and level system, scoring and persistence, the level select screen and the full menu flow are implemented. Remaining before the first release: touch input, the settings toggles, an audio pass and the WebGL build.

## Roadmap

- [x] Core grid and merge logic
- [x] Eleven-step fruit chain with per-fruit point values
- [x] Quantity-based level objectives
- [x] Fifteen measured levels with a sawtooth difficulty curve
- [x] Score and per-level high score tracking
- [x] Sequential level unlocking with persistent progress
- [x] Menu flow, level select, level completed and game over screens
- [x] Persistent background music and tier-scaled merge audio
- [ ] Touch and swipe input
- [ ] Settings toggles for music and sound effects
- [ ] New-record indication on the results screens
- [ ] Audio and polish pass
- [ ] WebGL build and release
- [ ] Move-limited level objectives
- [ ] Additional levels

## License

All rights reserved. Source code and assets in this repository may not be reused, redistributed, or repurposed without permission.

---

*The grid and merge logic build on a classic sliding-tile merge mechanic; the level system, objective design, scoring, fruit chain, art direction and UI are original to this project.*
