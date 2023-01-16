# Omega_Sudoku

A Sudoku solver made by Amit Barak using the Dancing Links (DLX) algorithm implemented in C#. This solver is able to find a solution for Sudoku puzzles, including boards ranging from 1x1 to 25x25 and handles invalid inputs. The project also includes unit testing.

#### How does it work?
The solver converts a Sudoku problem to an exact cover problem. The board is represented in a matrix of 0's and 1's. Each row represents the possibility of a certain value to exist in a certain place in the matrix. 1's exist in each row under four columns:

- Column constraint column: represents the possibility of the row's value to exist in the column on the value's place.
- Row constraint column: represents the possibility of the row's value to exist in the row on the value's place.
- Square constraint column: represents the possibility of the row's value to exist in the square on the value's place.
- Cell constraint column: represents the possibility of the row's value to exist in the cell on the value's place.

NOTE: When encountering an unknown value in the accepted board, we put a row for each possibility. When encountering a known value in the accepted matrix, we put a single row with ones and empty rows for the non-existent possibillity.

In my solution, I created a quadruply linked list that represents the exact cover matrix from the board without a middle stage of the former explained matrix. Each one is a node, and each zero doesn't get memory allocated to it. This linked list can be efficiently solved using a backtracking algorithm called Algorithm X.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

You will need the following to run the solver:

- .Net 6 SDK

### Installing

Clone the repository to your local machine:
```
  git clone https://github.com/amitbarak/sudoku-solver.git
```
Open the SudokuSolver2.sln using Visual Studio

### Running the solver

You can run the application by pressing F5 or using the Debug > Start Debugging menu. Additionally, you can run the application by opening SudokuSolver2 inside the repository and running:
```
  dotnet run
```
## How to Use the Project?
### How to use in the console?
#### Step one: choose where the file should be taken from, type the board if you haven't chosen a file.
Example:

```
where should the board be taken from?
c for console, f for file
c
type the board
800000070006010053040600000000080400003000700020005038000000800004050061900002000
```
#### Step two: choose if you want to save the resulted board into a file
Example:

```
do you want to save the board in a file?
type f for file or enter otherwise:
f
type the address of the file this will go into:
C:\Users\USER-HP1\Downloads\sudokuFile3.txt
```

example for a resulted board:
```
| 8  | 3  | 1  | 5  | 2  | 9  | 6  | 7  | 4
---------------------------------------------
| 7  | 9  | 6  | 8  | 1  | 4  | 2  | 5  | 3
---------------------------------------------
| 5  | 4  | 2  | 6  | 3  | 7  | 1  | 8  | 9
---------------------------------------------
| 1  | 5  | 9  | 7  | 8  | 3  | 4  | 2  | 6
---------------------------------------------
| 4  | 8  | 3  | 2  | 9  | 6  | 7  | 1  | 5
---------------------------------------------
| 6  | 2  | 7  | 1  | 4  | 5  | 9  | 3  | 8
---------------------------------------------
| 3  | 6  | 5  | 4  | 7  | 1  | 8  | 9  | 2
---------------------------------------------
| 2  | 7  | 4  | 9  | 5  | 8  | 3  | 6  | 1
---------------------------------------------
```


### How to use the code?

In order to validate and out put the result of a string there is a need to call:
```
HandleInput(string input, List<IWriter> resultWriters);
```

for example:
```
ConsoleHandler consoleHandler = new();
String input = UserHandlingHelper.GetInput(consoleHandler);

List<IWriter> resultWriters = new()
{
  consoleHandler
};
UserHandlingHelper.HandleInput(input, resultWriters);
```


In order to Solve a valid board there is a need to call DLXSolver.DancingLinksSolver.Solve(Board):
```
Board board = new Board(input);
board = DLXSolver.DancingLinksSolver.Solve(board);
//returns null if the board is unsolvble
```