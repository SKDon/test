﻿CREATE SEQUENCE [dbo].[BillNumberCounter]
		AS INT
		START WITH 1
		INCREMENT BY 1
		NO MAXVALUE
		CYCLE
		CACHE 10
