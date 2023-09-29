Product(expression)::=$\\$
CASE
    WHEN MIN(ABS($expression:(expression.Template)()$))=0 THEN 0
    ELSE EXP(SUM(LOG(ABS(NULLIF($expression:(expression.Template)()$, 0))))) 
            * CASE WHEN (COUNT(CASE WHEN $expression:(expression.Template)()$ < 0 THEN 1 ELSE NULL END) % 2)=0 
                    THEN 1 
                    ELSE -1 
              END  
END