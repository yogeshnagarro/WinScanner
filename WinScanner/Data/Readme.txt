# Identify NOC : (\b(\w+\s)?(NO|no)(\s)(\w+)?[OBJECTION|objection](\s?)(\w+)?[CERTIFICATE|certificate])|(\b(\w+\s)?(NOC|noc))
Region : \b(\w+\s)?(Region)\s((?i)west|east|north|south)

Validity Period: (\b(\w+\s)?((?i)(Validity)\s(period)\s))(\d.\.\d.\.\d{4})\s((?i)to)\s(\d.\.\d.\.\d{4})
Replace: $6 to $8

Validity Period:  \b(?<validity>(Validity)\s(Period)\s)(?<day>\d{1,2}).(?<month>\d{1,2}).(?<year>\d{4})\s(to)\s((?<today>\d{1,2}).(?<tomonth>\d{1,2}).(?<toyear>\d{4}))\b
Replace : $month $year ....

Time table: \b(?<day>\d{1,2}).(?<month>\d{1,2}).(?<year>\d{2})\s((?<today>\d{1,2}).(?<tomonth>\d{1,2}).(?<toyear>\d{2}))\s(?<hr>\d{1,2}).(?<min>\d{1,2})\s(?<toHr>\d{1,2}).(?<toMin>\d{1,2})(\s|(\s[|]\s))(?<capacity>\d{1,4}.\d{1,2})\b

Time table: \b((?<day>\d{1,2}).(?<month>\d{1,2}).(?<year>\d{2})\s((?<today>\d{1,2}).(?<tomonth>\d{1,2}).(?<toyear>\d{2}))\s(?<hr>\d{1,2}).(?<min>\d{1,2})\s(?<toHr>\d{1,2}).(?<toMin>\d{1,2})(\s|(\s[|]\s))(?<capacity>\d{1,4}.\d{1,2}))(?<SType>([\(]Regular)|(\s)([\(]Revised))\b