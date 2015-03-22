namespace Alicargo.DataAccess.DbContext
{
	internal enum SqlError
	{
		ViolationOfUniqueIndex = 2601,
		ViolationOfConstraint = 2627,
		DeleteStatementConflictedWihtConstraint = 547
	}
}