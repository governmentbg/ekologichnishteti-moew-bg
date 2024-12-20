using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Enums;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;
using Zopoesht2Migration.DbContexts.Zopoesht2;
using Zopoesht2Migration.DbContexts.Zopoesht2.Enums;
using Zopoesht2Migration.DbContexts.Zopoesht2.Models;

namespace Zopoesht2Migration.Migrations
{
    public class OperatorsMigrationService
    {
        public static void Start()
        {
            using (var zopoesht2Context = new Zopoesht2Context())
            {
                using (var zoposht1Context = new MosvEcologicalDamageContext())
                {
                    Console.WriteLine("=== Starting operators migration ===");
                    MigrateOperators(zopoesht2Context, zoposht1Context);
                    AddUnknownOperator(zopoesht2Context);
                    Console.WriteLine("=== Finished operators migration ===");
                }
            }
        }


        private static void MigrateOperators(Zopoesht2Context zopoesht2Context, MosvEcologicalDamageContext zoposht1Context)
        {
            var operatorCommits = zoposht1Context.OperatorCommits
                 .AsNoTracking()
                 .Include(e => e.OperatorBasicPart.Entity)
                 .Include(e => e.OperatorCorrespondenceDataPart.Entity)
                    .Where(e => (e.State == CommitState.Actual
                     || e.State == CommitState.Modification
                     || e.State == CommitState.Deleted
                     || e.State == CommitState.InitialDraft)
                     && e.OperatorBasicPart != null)
                    .ToList();

            var operatorsToAdd = new List<Operator>();

            using (var zopoeshtTransaction = zopoesht2Context.Database.BeginTransaction())
            {
                foreach (var operatorCommit in operatorCommits)
                {
                    if (operatorCommit.OperatorBasicPart == null)
                    {
                        continue;
                    }

                    var operatorEntity = operatorCommit.OperatorBasicPart.Entity;

                    var newOperator = new Operator();

                    newOperator = AddNewOperator(operatorEntity);

                    if (operatorCommit.OperatorCorrespondenceDataPart != null)
                    {
                        var operatorCorrespondenceEntity = operatorCommit.OperatorCorrespondenceDataPart.Entity;

                        var newOperatorCorrespondece = AddOperatorCorrespondenceData(operatorCorrespondenceEntity);

                        newOperator.Operatorcorrespondence = newOperatorCorrespondece;
                    }

                    operatorsToAdd.Add(newOperator);
                }

                zopoesht2Context.Operators.AddRange(operatorsToAdd);
                zopoesht2Context.SaveChanges();

                zopoeshtTransaction.Commit();
            }
        }

        private static void AddUnknownOperator(Zopoesht2Context zopoesht2Context)
        {
            var activities = zopoesht2Context.Activities
              .AsNoTracking()
              .ToList();

            var subActivities = zopoesht2Context.Subactivities
                .AsNoTracking()
                .ToList();

            using (var zopoeshtTransaction = zopoesht2Context.Database.BeginTransaction())
            {
                var unknownOperator = new Operator
                {
                    Firstname = null,
                    Middlename = null,
                    Lastname = null,
                    Type = OperatorType.LegalEntity,
                    Legalentityname = "Неизвестен оператор",
                    Legalentityuic = null,
                    Settlementid = null,
                    Managementaddress = null,
                    Postalcode = null,
                    Migrationid = null,
                    Name = "Неизвестен оператор",
                    Namealt = null,
                    Alias = "unknown",
                    Isactive = true,
                    Version = 0,
                    Operatorcorrespondence = new Operatorcorrespondence()
                };

                zopoesht2Context.Operators.Add(unknownOperator);
                zopoesht2Context.SaveChanges();

                var addedUnknownOperator = zopoesht2Context.Operators.SingleOrDefault(e => e.Alias == "unknown");

                unknownOperator.Activityofferings = AddUnknowOperatorActivities(activities, subActivities, addedUnknownOperator.Id);

                zopoesht2Context.SaveChanges();

                zopoeshtTransaction.Commit();
            }
        }

        private static ICollection<Activityoffering> AddUnknowOperatorActivities(List<DbContexts.Zopoesht2.Models.Activity> activities, List<Subactivity> subactivities, int operatorId)
        {
            var activityOfferingsToAdd = new List<Activityoffering>();

            foreach (var activity in activities)
            {
                var subActivitiesToAdd = subactivities.Where(e => e.Activityid == activity.Id);

                foreach (var subactivity in subActivitiesToAdd)
                {
                    var newActivityOffering = new Activityoffering
                    {
                        Activityid = activity.Id,
                        Subactivityid = subactivity.Id,
                        Operatorid = operatorId,
                        Authoritybasinid = null,
                        Authorityriosvid = null,
                        Terrainid = null
                    };
                    activityOfferingsToAdd.Add(newActivityOffering);
                }
            }


            return activityOfferingsToAdd;
        }

        private static Operatorcorrespondence AddOperatorCorrespondenceData(OperatorCorrespondenceDatum operatorCorrespondenceEntity)
        {
            var newOperatorCorrespondence = new Operatorcorrespondence
            {
                Settlementid = operatorCorrespondenceEntity.SettlementId,
                Phone = operatorCorrespondenceEntity.Phone,
                Fax = operatorCorrespondenceEntity.Fax,
                Email = operatorCorrespondenceEntity.Email,
                Contactperson = operatorCorrespondenceEntity.ContactPerson,
                Postalcode = operatorCorrespondenceEntity.PostalCode,
                Correspondenceaddress = ConstructAddress(operatorCorrespondenceEntity.Neighborhood, operatorCorrespondenceEntity.Street, operatorCorrespondenceEntity.StreetNumber,
                operatorCorrespondenceEntity.ApartmentBuildingNumber, operatorCorrespondenceEntity.ApartmentBuildingEntrance, operatorCorrespondenceEntity.ApartmentBuildingFloor,
                operatorCorrespondenceEntity.ApartmentBuildingFlat)
            };

            return newOperatorCorrespondence;
        }

        private static Operator AddNewOperator(OperatorBasic operatorEntity)
        {
            var newOperator = new Operator
            {
                Firstname = operatorEntity.FirstName,
                Middlename = operatorEntity.MiddleName,
                Lastname = operatorEntity.LastName,
                Type = (OperatorType)operatorEntity.Type,
                Legalentityname = operatorEntity.LegalEntityName,
                Legalentityuic = operatorEntity.LegalEntityUic,
                Settlementid = operatorEntity.SettlementId,
                Managementaddress = ConstructAddress(operatorEntity.Neighborhood, operatorEntity.Street, operatorEntity.StreetNumber, operatorEntity.ApartmentBuildingNumber,
                                    operatorEntity.ApartmentBuildingEntrance, operatorEntity.ApartmentBuildingFloor, operatorEntity.ApartmentBuildingFlat),
                Postalcode = operatorEntity.PostalCode,
                Migrationid = operatorEntity.Id,
                Name = (OperatorType)operatorEntity.Type == OperatorType.Person ?
                       operatorEntity.FirstName + " " + operatorEntity.MiddleName + " " + operatorEntity.LastName :
                       operatorEntity.LegalEntityName,
                Namealt = null,
                Alias = null,
                Isactive = true,
                Version = 0,
            };

            return newOperator;
        }

        private static string? ConstructAddress(string? neighborhood, string? street, string? streetNumber, string? apartmentBuildingNumber,
            string? apartmentBuildingEntrance, string? apartmentBuildingFloor, string? apartmentBuildingFlat)
        {
            string? address = null;

            if (!string.IsNullOrWhiteSpace(neighborhood))
            {
                address = neighborhood;
            }

            if (!string.IsNullOrWhiteSpace(street))
            {
                address += " " + street;
            }

            if (!string.IsNullOrWhiteSpace(streetNumber))
            {
                address += " " + streetNumber;
            }

            if (!string.IsNullOrWhiteSpace(apartmentBuildingNumber))
            {
                address += apartmentBuildingNumber.Contains("бл") ? " " + apartmentBuildingNumber : " бл. " + apartmentBuildingNumber;
            }

            if (!string.IsNullOrWhiteSpace(apartmentBuildingEntrance))
            {
                address += apartmentBuildingEntrance.Contains("вх") ? " " + apartmentBuildingEntrance : " вх. " + apartmentBuildingEntrance;
            }

            if (!string.IsNullOrWhiteSpace(apartmentBuildingFloor))
            {
                address += apartmentBuildingFloor.Contains("ет") ? " " + apartmentBuildingFloor : " ет. " + apartmentBuildingFloor;
            }

            if (!string.IsNullOrWhiteSpace(apartmentBuildingFlat))
            {
                address += apartmentBuildingFlat.Contains("ап") ? " " + apartmentBuildingFlat : " ап. " + apartmentBuildingFlat;
            }

            return address;
        }
    }
}
