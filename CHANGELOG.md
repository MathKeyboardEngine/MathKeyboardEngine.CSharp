## [1.0.0-beta] 2023-11-20

## Added

- `Parse.Latex`. Developing this feature - parsing a LaTeX string for editing by MathKeyboardEngine - started after a [question thread](https://github.com/orgs/MathKeyboardEngine/discussions/1) was opened by [chengyi](https://github.com/WCY91).
- `Insert` previously only accepted a single `TreeNode`, but it can now handle an `IEnumerable<TreeNode>` too.


## [0.2.0] 2023-01-27

### Changed

- Renamed `DeleteCurrent` to `DeleteLeft`.

### Added

- `DeleteRight`.

## [0.1.1] 2022-11-23

- Fix only relevant if `LatexConfiguration`'s `ActivePlaceholderColor` and `PassivePlaceholderColor` is used: use `{\color{my-color}x}` instead of `\color{my-color}{x}`.

## [0.1.0] 2022-06-28

- First release.