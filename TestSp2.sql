/* 
    SignedComment: ## Test2 Stored Procedure
    This is an example stored procedure that demonstrates
    the use of various custom annotations for converting
    to a Jupyter notebook.
*/

CREATE PROCEDURE Test2
AS
BEGIN
    -- SignedComment: ### Step 1: Create a Common Table Expression (CTE)

    -- NewCellBegin_1
    WITH cte_example AS (
    --NewCellBegin_0
        SELECT
            Column1,
            Column2,
            Column3
        FROM
            SomeTable
        -- DemoWhere: Where Column1 = 'example'
    --NewCellEnd_0
    )
    -- SignedComment: ### Step 2: Insert data into a temporary table
    -- this temp table will be used in the final SELECT


    SELECT
        Column1,
        Column2,
        Column3
            INTO #TempTable -- NewBlockToComment_1
    FROM
        cte_example
    -- DemoWhere: WHERE Column2 > 100
    -- NewCellEnd_1

    /* 
        SignedComment: ### Step 3: Perform a final SELECT
        this is a line below a SignedComment
        this is a second line below a SignedComment
    */

    /* 
    --perform this select in the notebook
        --NewCellBegin_5
                SELECT
        Column1,
        Column2,
        Column3
            INTO #TempTable
        FROM
            cte_example


        --NewCellEnd_5
    */

    -- SignedComment: ### Step 3: Perform a final SELECT
    -- this is a line below a SignedComment
    -- this is a second line below a SignedComment

    -- NewCellBegin_2
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

    -- NewCellEnd_2


    -- SignedComment: #### CTE example where 2 CTEs are used and a final SELECT is performed
    -- SignedComment: This is a second line of SignedComment


    -- NewCellBegin_3
        -- NewCellBegin_6
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
        -- NewCellBegin_4
        SELECT
            Column1,
            Column2,
            Column3
        FROM
            SomeTable
        -- DemoWhere: WHERE Column1 = 'example2'
        -- NewCellEnd_4
    -- NewCellEnd_6
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
    -- NewCellEnd_3
END
