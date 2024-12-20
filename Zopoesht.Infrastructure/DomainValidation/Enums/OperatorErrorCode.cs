namespace Zopoesht.Infrastructure.DomainValidation.Enums
{
    public enum OperatorErrorCode
    {
        Operator_DoesNotExist = 401,
        Operator_TerrainDoesNotExist = 402,
        Operator_TerrainDoesNotBelongToTheOperator = 403,
        Operator_SubActivityDoesNotBelongToTheActivity = 404,
        Operator_ActivityOfferingDoesNotExist = 405,
        Operator_SubActivityDoesNotExist = 406
    }
}
