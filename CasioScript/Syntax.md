# Notes

* When i write < in the syntax, it means it is a comparison, so it can be any of the following: <, >, <=, >=, ==, !=

| Syntax   | Description                     | Compiles to |
|----------|---------------------------------|-------------|
| `X < Y`  | X is less than Y                | `X<Y`       |
| `X > Y`  | X is greater than Y             | `X>Y`       |
| `X <= Y` | X is less than or equal to Y    | `X≤Y`       |
| `X >= Y` | X is greater than or equal to Y | `X≥Y`       |
| `X == Y` | X is equal to Y                 | `X=Y`       |
| `X != Y` | X is not equal to Y             | `X≠Y`       |

* When you write a variable name, it must be capitalized in the compiled code, so `x` will be `X` in the compiled code.

# Variable

#### Variable Declaration
| Syntax         | Description                                             | Compiles to                  |
|----------------|---------------------------------------------------------|------------------------------|
| `X = 1;`       | Declares a variable named `X` with the value `1`        | `1->X`                       |
| `X, Y, z = 1;` | Declares variables `X`, `Y`, and `z` with the value `1` | `1->X`<br/>`X->Y`<br/>`Y->Z` |

#### Variable Modification
| SYntax       | Description                                     | Compiles to |
|--------------|-------------------------------------------------|-------------|
| `X = 1;`     | Sets the value of `x` to `1`                    | `1->X`      |
| `X = y;`     | Sets the value of `X` to the value of `y`       | `y->X`      |
| `X += Y;`    | Adds `Y` to the value of `X`                    | `X+1->X`    |
| `X -= Y;`    | Subtracts `Y` from the value of `X`             | `X-1->X`    |
| `X *= Y;`    | Multiplies the value of `X` by `Y`              | `X*Y->X`    |
| `X /= Y;`    | Divides the value of `X` by `Y`                 | `X/Y->X`    |
| `X = Y + Z;` | Sets the value of `X` to the sum of `Y` and `Z` | `Y+Z->X`    |

# Loops

#### For Loop
X and Y are variables or numbers, when one is a variable it has to be declared before the loop.

X is included, so when X = 5, the loop will run 5 times.

| Syntax                                 | Description   | Compiles to                             |
|----------------------------------------|---------------|-----------------------------------------|
| `for I in X`<br/>`{`<br/>`...`<br/>`}` | Loops X times | `For 1->I To X` <br> `...` <br/> `Next` |

#### While Loop
X and Y are variables or numbers, when one is a variable it has to be declared before the loop.

| Syntax                                   | Description        | Compiles to                              |
|------------------------------------------|--------------------|------------------------------------------|
| `while X < Y`<br/>`{`<br/>`...`<br/>`}`  | Loops while X < y  | `While X<Y` <br> `...` <br/> `WhileEnd`  |

#### Do While Loop
X and Y are variables or numbers, when one is a variable it has to be declared before the loop.
The code will also run at least once, since the check happens after the code ran once.

| Syntax                                            | Description        | Compiles to                         |
|---------------------------------------------------|--------------------|-------------------------------------|
| `do`<br/>`{`<br/>`...`<br/>`}`<br/>`while X < Y`  | Loops while X < y  | `do` <br> `...` <br/> `LpWhle X<Y`  |

#### goto
X is not a variable, it's a number, so it doesn't have to be declared before the loop but must be unique.

| Syntax     | Description     | Compiles to |
|------------|-----------------|-------------|
| `label X;` | Sets a label X  | `Lbl X`     |
| `goto X;`  | Goes to label X | `Goto X`    |

# Selection Statements

#### If Statement
X and Y are variables or numbers, when one is a variable it has to be declared before the loop.

The if statement can be used with or without an else statement.

| Syntax                               | Description | Compiles to                                    |
|--------------------------------------|-------------|------------------------------------------------|
| `if X < Y`<br/>`{`<br/>`...`<br/>`}` | If X < y    | `If X<Y` <br> `Then` <br/> `...` <br/> `IfEnd` |
| `else`<br/>`{`<br/>`...`<br/>`}`     | Else        | `Else` <br/> `...` <br/> `IfEnd`               |
| `if X < Y => ...`                    | If X < y    | `If X<Y => ...`                                |


# Text Display

#### Placing Text
Everything between the quotes is displayed on the screen.

| Syntax                 | Description         | Compiles to          |
|------------------------|---------------------|----------------------|
| `Locate(x,y,"Hello");` | Hello 21x7 screen   | `Locate x,y,"Hello"` |
| `Text(x,y,"Hello");`   | Hello 127,63 screen | `Text x,y,"Hello"`   |
| `ClearText();`         | Clears the screen   | `ClrText`            |


# Input

#### Input
X is a variable, but it will be the value of the input, so it doesn't have to be declared before the loop.

| Syntax                             | Description                                       | Compiles to   |
|------------------------------------|---------------------------------------------------|---------------|
| `X = GetInput();`                  | Input                                             | `?->X`        |
| `X = GetInputWithPrompt("Hello");` | Input with prompt (awaits for the press of [exe]) | `"Hello"?->X` |
| `X = GetKey();`                    | Gets the currently pressed key (use in a loop)    | `Getkey->X`   |

