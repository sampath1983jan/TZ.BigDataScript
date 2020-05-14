grammar AScript;
 
/*
 * Parser Rules
 */
statement : line*;
line : var ':'  data DOT sel select ';'  
| var ':'  data DOT gp groupby ';'
| var ':' data DOT 'join' LFBRKT join RFBRKT ';' 
| var ':' data DOT 'cube'  cube';'
| var ':' data DOT 'rollup'  rollup';'
| var ':' data DOT dist ';'
| var ':' data DOT limit ';'
| var ':' data DOT union ';'
| var ':' data DOT intersect ';' 
| var ':' data DOT except ';' 
| var ':' data DOT drop ';' 
| var ':' data DOT withcolumn ';' 
| var ':' load ';' 
| var ':' data filtercondition ';' 
| var ':' data sortstatement ';'
| var ':' data DOT export ';'
| var ':' data DOT nullreplace ';'
| var ':' data DOT stringreplace ';'
| var ':' data DOT nadrop ';'
| declare ':' var ':'  param ';'
| declare ':' var ':'  TEXT ';'
| declare ':' var ':'  todata ';'
| dr ':' path ';'
| ns ':' TEXT ';'
| nsp ':' param ';'
| imp ':' imptext ';'
;

var:WORD;
strfield:WORD;
strvalue:TEXT;
strreplacevalue:TEXT;
dr:'dataroot';
ns:'namespace';
nsp:'namespaceparam';
imptext:(TEXT)(',' (TEXT))*;
imp:'import';
nullreplace:'nullrep' LFBRKT strfield ',' strvalue RFBRKT;
stringreplace:'textrep' LFBRKT strfield ',' strvalue ',' strreplacevalue RFBRKT;
nadrop:'nulldrop' LFBRKT TEXT RFBRKT;
data:WORD;
dataformat:WORD ',' INT;
comparedate:WORD;
adddate:dataformat;
datediff:WORD(','comparedate);
monthadd:WORD ',' INT;
monthbetween:WORD(','comparedate);
next:WORD(','WEEK);
formatingstring:WORD;
todate:WORD(',' TEXT);
totime:WORD(',' TEXT);
dateformat:WORD(','formatingstring);
datefunction: DATEFUNCTION LFBRKT WORD RFBRKT;
dateadvancefunction: (DATEUDF LFBRKT (monthadd | adddate | monthbetween | datediff | next | dateformat | todate | totime)  RFBRKT) ;
stringfunction:STRINGUDF LFBRKT WORD RFBRKT;
stringformat:STRINGUDF LFBRKT TEXT (',' WORD) RFBRKT ;
numberfunction: (NUMBERUDF LFBRKT WORD RFBRKT) ;
numberformat:NUMBERUDF LFBRKT WORD (',' INT) RFBRKT;
strings:WORD | (WORD '@' WORD);
stringltr: strings| (datefunction '@' WORD) | (dateadvancefunction '@' WORD) | (stringfunction '@' WORD)| (stringformat '@' WORD) | (numberfunction '@' WORD)| (numberformat '@' WORD) ;
fields:stringltr(','stringltr)*;
sel:SELECT;
gp:GROUPBY;
eos:EOS;
dataframe:WORD;
flt: FILTER;
param: '#' WORD '#';
todata:'todc' LFBRKT dataitem RFBRKT;
dataitem:TEXT;
load:'load' LFBRKT path RFBRKT DOT schema LFBRKT schemaname RFBRKT DOT 'fields' LFBRKT datafields RFBRKT;
export:'export' LFBRKT path RFBRKT;
sort: SORT;
dist: 'distinct';
isall:'all';
limit: 'limit' LFBRKT limitvalue RFBRKT;
intersect: 'intersect' LFBRKT data RFBRKT (DOT isall)?;
except: 'except' LFBRKT data RFBRKT (DOT isall)?;
union: 'union' LFBRKT data RFBRKT (DOT isall)?;  
drop: 'drop' LFBRKT fields RFBRKT;
newcase: (WORD '===' TEXT) | (WORD DOT STRINGUDF) ;
joincase: (OPER newcase) ;
multicase:(LFBRKT newcase ',' TEXT RFBRKT) | LFBRKT newcase (joincase)* ',' TEXT RFBRKT;
multiwhen:('when' multicase)(',' ('when' multicase))*;
newcolumn:multiwhen '@' WORD; 
context:TEXT;
conlit:TEXT;
concatstate:(conlit)(',' (conlit))*;
concatcolumn:concatstate '@' WORD;
withcolumn:'new' LFBRKT (newcolumn | concatcolumn)  RFBRKT;
path:TEXT;
cube:LFBRKT fields RFBRKT DOT aggtype LFBRKT RFBRKT  filtercondition? sortstatement? ;
rollup:LFBRKT fields RFBRKT DOT aggtype LFBRKT RFBRKT  filtercondition? sortstatement? ;
select: LFBRKT fields RFBRKT filtercondition? sortstatement?;
groupby: LFBRKT fields RFBRKT DOT LBAGG LFBRKT agg RFBRKT filtercondition? sortstatement? ;
filtercondition:DOT flt LFBRKT condition RFBRKT ;
sortstatement:DOT sort LFBRKT order RFBRKT;
join:data ',' LFBRKT multijoin RFBRKT ',' JOINTYPE;
multijoin:(LFBRKT relation RFBRKT)(',' (LFBRKT relation RFBRKT))*;
relation:joindata DOT joinfield '=' joineddata DOT joinedfield;
aggtype:AGGRE;
joindata:data;
joinfield:data;
joinedfield:data;
joineddata:data;
declare:DECLARTYPE;
datafields:(TEXT ':' DATATYPE)(',' (TEXT ':' DATATYPE))*;
order:(ORDERBY LFBRKT fields RFBRKT)(',' (ORDERBY LFBRKT fields RFBRKT))*;
condition:(WORD CONDITIONOPERATOR TEXT)(',' (WORD CONDITIONOPERATOR TEXT))*;
agg:(WORD DOT AGGRE)(',' (WORD DOT AGGRE))*;
selectsort: (order);
limitvalue: INT;
schema:'table';
schemaname:TEXT;


/*
 * Lexer Rules
 */
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
fragment DIGIT: [0-9];
fragment LETTER: [a-zA-Z]; 
fragment COMMA : (',');
fragment STRING : [a-zA-Z0-9];
fragment SPLIT:(':');

LBAGG : 'Agg';
SELECT : 'select' ;
GROUPBY : 'groupby';
CUBE : 'cube';
ROLLUP : 'rollup';
FILTER : 'search';
SORT : 'sortby';
JOIN : 'join';
ORDERBY: 'asc' | 'desc';
CONDITIONOPERATOR: '+' | '-' | '<' | '>' | '==' | '>=' | '<=' | '!=' | '#' | 'isblank' | '/';
DATEFUNCTION: 'year' | 'month' | 'day' | 'minute' | 'sec' | 'weekno' | 'hour' | 'last' | 'quarter' ;
DATEUDF:'dateadd' | 'datediff' | 'addmonth' | 'monthbetween' | 'next' |'dformat' | 'todate' | 'totime'; 
STRINGUDF: 'lower' | 'trim' | 'upper' | 'length' | 'sformat' | 'isnull' | 'isnan' | 'isnotnull' | 'isempty' | 'nafill' | 'nadrop' | 'replace';
NUMBERUDF: 'nformat' | 'round' | 'floor' | 'ceil';
JOINTYPE :  'inner' | 'full'  | 'left' | 'right' | 'cross';
DECLARTYPE:'v' | 'p' | 'dc';
AGGRE : 'avg' | 'sum' | 'min' | 'max' | 'sd' | 'davg' | 'count' | 'dcount';
DATATYPE : 'number' | 'double' | 'decimal' | 'string' | 'date' | 'bool';
WEEK: 'Mon' | 'Tue' | 'Wed' | 'Thu' | 'Fri' | 'Sat' | 'Sun';
OPER: '||' | '&&';
SPLITER: (':');
INT             :  '0'..'9'+;
DOUBLE : '0'..'9'+'.''0'..'9'+;
WORD                : (LOWERCASE | UPPERCASE | DIGIT | '_' | '*' )+ ; 
 
LFSBRKT                 :('[');
RFSBRKT                 :(']');
LFBRKT                 :('(');
RFBRKT          :(')');
DOT             :('.');
STRING_LITERAL: ~[\\\r\n"] ;
AGGGROUP:(SPLITER AGGRE);
SAYS:('*');
EOS:(';');
TEXT                : ('"'.*?'"' );
WHITESPACE  : (' ' | '\t' | '\r' | '\n')+ ->skip;
WS  : (' ');



 
 


 



