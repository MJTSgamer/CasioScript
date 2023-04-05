grammar CasioScript;

program: line* EOF;

line: statement | ifBlock | whileBlock | block | comment;

comment: COMMENT | ML_COMMENT;

statement: (assignment | functionCall) NEWLINES;

ifBlock: 'if' expression block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock;

whileBlock: WHILE expression block ('else' elseIfBlock);
WHILE: 'while' | 'until';

assignment: IDENTIFIER '=' expression;

functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';

block: '{' line* '}';

expression
    : constant                                  #constantExpression
    | IDENTIFIER                                #identifierExpression
    | functionCall                              #functionCallExpression
    | '(' expression ')'                        #parenExpression
    | '!' expression                            #notExpression
    | expression mulOp expression               #mulExpression
    | expression addOp expression               #addExpression
    | expression compOp expression              #compExpression
    | expression boolOp expression              #boolExpression
    ;
    
mulOp: '*' | '/';
addOp: '+' | '-';
compOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: 'and' | 'or' | 'xor' | '||' | '&&';

constant: INTERGER | FLOAT | STRING | BOOL | NULL;

INTERGER: [0-9]+;
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' (~["] ~[\r\n])* '"';
BOOL: 'true' | 'false' | 'True' | 'False';
NULL: 'null';

NEWLINES: [\r\n]+;
WS: [ \t]+ -> skip;
IDENTIFIER: [a-zA-Z];

COMMENT: '//' ~[\r\n]* -> skip; // single-line comment
ML_COMMENT: '/*' .*? '*/' -> skip; // multi-line comment