/* 
    BLocCom: ## Test2 Stored Procedure
    This is an example stored procedure that demonstrates
    the use of various custom annotations for converting
    to a Jupyter notebook.
*/

CREATE PROCEDURE Test2
AS
BEGIN
    -- BLocCom: ### Step 1: Create a Common Table Expression (CTE)

    -- CodeBlocBegin_1
    WITH cte_example AS ( -- RemoveLine_Block_1
    --CodeBlocBegin_0
        SELECT
            Column1,
            Column2,
            Column3
        FROM
            SomeTable
        -- DemoWhere: Where Column1 = 'example'
    --CodeBlocEnd_0
    )
    -- BLocCom: ### Step 2: Insert data into a temporary table
    -- this temp table will be used in the final SELECT


    SELECT
        Column1,
        Column2,
        Column3
            INTO #TempTable -- BlocToComment_1
    FROM
        cte_example
    -- DemoWhere: WHERE Column2 > 100
    -- CodeBlocEnd_1

    /* 
        BLocCom: ### Step 3: Perform a final SELECT
        this is a line below a BLocCom
        this is a second line below a BLocCom
    */

    /* 
    --perform this select in the notebook
        --CodeBlocBegin_5
                SELECT
        Column1,
        Column2,
        Column3
            INTO #TempTable
        FROM
            cte_example


        --CodeBlocEnd_5
    */

    -- BLocCom: ### Step 3: Perform a final SELECT
    -- this is a line below a BLocCom
    -- this is a second line below a BLocCom

    -- CodeBlocBegin_2
    SELECT
        Column1,
        Column2,
        SUM(Column3) AS TotalColumn3
    FROM
        #TempTable
    -- DemoWhere: WHERE TotalColumn3 > 1000
    GROUP BY
        Column1,
        Column2

    -- CodeBlocEnd_2


    -- BLocCom: #### CTE example where 2 CTEs are used and a final SELECT is performed
    -- BLocCom: This is a second line of BLocCom


    -- CodeBlocBegin_3
        -- CodeBlocBegin_6
            /*
                This is a comment for the CTE
            */

    ;WITH cte_example AS (
        SELECT
            Column1,
            Column2,
            Column3
        FROM
            SomeTable
        -- DemoWhere: WHERE Column1 = 'example'
    )
    ,cte_example2 AS ( -- RemoveLine_Block_6
        -- CodeBlocBegin_4
        SELECT
            Column1,
            Column2,
            Column3
        FROM
            SomeTable
        -- DemoWhere: WHERE Column1 = 'example2'
        -- CodeBlocEnd_4
    -- CodeBlocEnd_6
    )
    SELECT
        Column1,
        Column2,
        SUM(Column3) AS TotalColumn3
    FROM
        cte_example
    INNER JOIN
        cte_example2
    ON
        cte_example.Column1 = cte_example2.Column1

    -- DemoWhere: WHERE TotalColumn3 > 1000

    GROUP BY
        Column1,
        Column2
    -- CodeBlocEnd_3

    -- CodeBlocBegin_10
    ;With a as (
        -- CodeBlocBegin_7
        select 1 as a, 2 as b
        -- CodeBlocEnd_7
    )
    ,b as ( -- RemoveLine_Block_10
        -- CodeBlocBegin_8
        select 1 as a, 2 as b
        -- CodeBlocEnd_8
    -- CodeBlocEnd_10
    )


    select * from a




END
