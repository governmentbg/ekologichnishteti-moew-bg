using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "acttype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acttype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "administrativeunit",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    administrativeunittype = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrativeunit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "affectedtype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affectedtype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationhistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    userfullname = table.Column<string>(type: "text", nullable: true),
                    modificationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    applicationid = table.Column<int>(type: "integer", nullable: false),
                    rootid = table.Column<int>(type: "integer", nullable: false),
                    applicationtype = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationhistory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationonedamage",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    occurenceconsequences = table.Column<string>(type: "text", nullable: true),
                    measurestaken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationonedamage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationonelegalentity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "text", nullable: true),
                    applicantviolations = table.Column<string>(type: "text", nullable: true),
                    recoveryadvice = table.Column<string>(type: "text", nullable: true),
                    allegedoccurenceconsequences = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationonelegalentity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "applicationonethreat",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    preventivemeasurestaken = table.Column<string>(type: "text", nullable: true),
                    analyticprotocols = table.Column<string>(type: "text", nullable: true),
                    measuresadvice = table.Column<string>(type: "text", nullable: true),
                    financialstatement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationonethreat", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authority",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authority", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "code",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_code", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameen = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "district",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code2 = table.Column<string>(type: "text", nullable: true),
                    secondlevelregioncode = table.Column<string>(type: "text", nullable: true),
                    mainsettlementcode = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emailtype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject = table.Column<string>(type: "text", nullable: true),
                    body = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailtype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "expensetype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expensetype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "individual",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    middlename = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_individual", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "insurance",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legalentity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    legalentityname = table.Column<string>(type: "text", nullable: true),
                    uin = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_legalentity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "measuretype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measuretype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "section",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_section", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sector",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sector", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "zopoeshtattachedfile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: true),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    mimetype = table.Column<string>(type: "text", nullable: true),
                    dbid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zopoeshtattachedfile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subactivity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    activityid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subactivity", x => x.id);
                    table.ForeignKey(
                        name: "FK_subactivity_activity_activityid",
                        column: x => x.activityid,
                        principalTable: "activity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "municipality",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    districtid = table.Column<int>(type: "integer", nullable: false),
                    code2 = table.Column<string>(type: "text", nullable: true),
                    mainsettlementcode = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipality", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipality_district_districtid",
                        column: x => x.districtid,
                        principalTable: "district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "email",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typeid = table.Column<int>(type: "integer", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: true),
                    body = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email", x => x.id);
                    table.ForeignKey(
                        name: "FK_email_emailtype_typeid",
                        column: x => x.typeid,
                        principalTable: "emailtype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    passwordsalt = table.Column<string>(type: "text", nullable: true),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    middlename = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    roleid = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    authorityid = table.Column<int>(type: "integer", nullable: true),
                    position = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_authority_authorityid",
                        column: x => x.authorityid,
                        principalTable: "authority",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_role_roleid",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "settlement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    municipalityid = table.Column<int>(type: "integer", nullable: false),
                    districtid = table.Column<int>(type: "integer", nullable: false),
                    municipalitycode = table.Column<string>(type: "text", nullable: true),
                    districtcode = table.Column<string>(type: "text", nullable: true),
                    municipalitycode2 = table.Column<string>(type: "text", nullable: true),
                    districtcode2 = table.Column<string>(type: "text", nullable: true),
                    typename = table.Column<string>(type: "text", nullable: true),
                    settlementname = table.Column<string>(type: "text", nullable: true),
                    typecode = table.Column<string>(type: "text", nullable: true),
                    mayoraltycode = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    altitude = table.Column<string>(type: "text", nullable: true),
                    isdistrict = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settlement", x => x.id);
                    table.ForeignKey(
                        name: "FK_settlement_district_districtid",
                        column: x => x.districtid,
                        principalTable: "district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_settlement_municipality_municipalityid",
                        column: x => x.municipalityid,
                        principalTable: "municipality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emailaddressee",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    emailid = table.Column<int>(type: "integer", nullable: false),
                    addresseetype = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    sentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    attemptscounter = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailaddressee", x => x.id);
                    table.ForeignKey(
                        name: "FK_emailaddressee_email_emailid",
                        column: x => x.emailid,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationlock",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtype = table.Column<int>(type: "integer", nullable: false),
                    applicationid = table.Column<int>(type: "integer", nullable: false),
                    lockeddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    unlockeddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    islocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationlock", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationlock_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "passwordtoken",
                columns: table => new
                {
                    value = table.Column<string>(type: "text", nullable: false),
                    expirationtime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isused = table.Column<bool>(type: "boolean", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passwordtoken", x => x.value);
                    table.ForeignKey(
                        name: "FK_passwordtoken_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "operatorcorrespondence",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    settlementid = table.Column<int>(type: "integer", nullable: true),
                    correspondenceaddress = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    fax = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    contactperson = table.Column<string>(type: "text", nullable: true),
                    postalcode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operatorcorrespondence", x => x.id);
                    table.ForeignKey(
                        name: "FK_operatorcorrespondence_settlement_settlementid",
                        column: x => x.settlementid,
                        principalTable: "settlement",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "operator",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    middlename = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    legalentityname = table.Column<string>(type: "text", nullable: true),
                    legalentityuic = table.Column<string>(type: "text", nullable: true),
                    settlementid = table.Column<int>(type: "integer", nullable: true),
                    managementaddress = table.Column<string>(type: "text", nullable: true),
                    postalcode = table.Column<string>(type: "text", nullable: true),
                    migrationid = table.Column<int>(type: "integer", nullable: true),
                    operatorcorrespondenceid = table.Column<int>(type: "integer", nullable: false),
                    administrativeunitriosvid = table.Column<int>(type: "integer", nullable: false),
                    administrativeunitbasinid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operator", x => x.id);
                    table.ForeignKey(
                        name: "FK_operator_administrativeunit_administrativeunitbasinid",
                        column: x => x.administrativeunitbasinid,
                        principalTable: "administrativeunit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_operator_administrativeunit_administrativeunitriosvid",
                        column: x => x.administrativeunitriosvid,
                        principalTable: "administrativeunit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_operator_operatorcorrespondence_operatorcorrespondenceid",
                        column: x => x.operatorcorrespondenceid,
                        principalTable: "operatorcorrespondence",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_operator_settlement_settlementid",
                        column: x => x.settlementid,
                        principalTable: "settlement",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "applicant",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicanttype = table.Column<int>(type: "integer", nullable: false),
                    operatorid = table.Column<int>(type: "integer", nullable: true),
                    authorityid = table.Column<int>(type: "integer", nullable: true),
                    individualid = table.Column<int>(type: "integer", nullable: true),
                    legalentityid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicant", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicant_authority_authorityid",
                        column: x => x.authorityid,
                        principalTable: "authority",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicant_individual_individualid",
                        column: x => x.individualid,
                        principalTable: "individual",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicant_legalentity_legalentityid",
                        column: x => x.legalentityid,
                        principalTable: "legalentity",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicant_operator_operatorid",
                        column: x => x.operatorid,
                        principalTable: "operator",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "applicationonebasic",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    incomingdocnumber = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    operatorid = table.Column<int>(type: "integer", nullable: true),
                    haswaterdamage = table.Column<bool>(type: "boolean", nullable: false),
                    hassoildamage = table.Column<bool>(type: "boolean", nullable: false),
                    hasspeciesdamage = table.Column<bool>(type: "boolean", nullable: false),
                    occurencelocation = table.Column<string>(type: "text", nullable: true),
                    occurencereasons = table.Column<string>(type: "text", nullable: true),
                    additionalinformation = table.Column<string>(type: "text", nullable: true),
                    hasinternationalelement = table.Column<bool>(type: "boolean", nullable: false),
                    internationalelementdescription = table.Column<string>(type: "text", nullable: true),
                    countryid = table.Column<int>(type: "integer", nullable: true),
                    countryauthority = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationonebasic", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationonebasic_country_countryid",
                        column: x => x.countryid,
                        principalTable: "country",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicationonebasic_operator_operatorid",
                        column: x => x.operatorid,
                        principalTable: "operator",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "terrain",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    districtid = table.Column<int>(type: "integer", nullable: true),
                    municipalityid = table.Column<int>(type: "integer", nullable: true),
                    settlementid = table.Column<int>(type: "integer", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    operatorid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terrain", x => x.id);
                    table.ForeignKey(
                        name: "FK_terrain_district_districtid",
                        column: x => x.districtid,
                        principalTable: "district",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_terrain_municipality_municipalityid",
                        column: x => x.municipalityid,
                        principalTable: "municipality",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_terrain_operator_operatorid",
                        column: x => x.operatorid,
                        principalTable: "operator",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_terrain_settlement_settlementid",
                        column: x => x.settlementid,
                        principalTable: "settlement",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "applicationone",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationonetype = table.Column<int>(type: "integer", nullable: false),
                    applicantid = table.Column<int>(type: "integer", nullable: false),
                    applicationonebasicid = table.Column<int>(type: "integer", nullable: false),
                    applicationonelegalentityid = table.Column<int>(type: "integer", nullable: true),
                    applicationonethreatid = table.Column<int>(type: "integer", nullable: true),
                    applicationonedamageid = table.Column<int>(type: "integer", nullable: true),
                    commitstate = table.Column<int>(type: "integer", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    changestatedescription = table.Column<string>(type: "text", nullable: true),
                    rootid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationone", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationone_applicant_applicantid",
                        column: x => x.applicantid,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationone_applicationonebasic_applicationonebasicid",
                        column: x => x.applicationonebasicid,
                        principalTable: "applicationonebasic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationone_applicationonedamage_applicationonedamageid",
                        column: x => x.applicationonedamageid,
                        principalTable: "applicationonedamage",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicationone_applicationonelegalentity_applicationonelega~",
                        column: x => x.applicationonelegalentityid,
                        principalTable: "applicationonelegalentity",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicationone_applicationonethreat_applicationonethreatid",
                        column: x => x.applicationonethreatid,
                        principalTable: "applicationonethreat",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "applicationoneterritorialrange",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationonebasicid = table.Column<int>(type: "integer", nullable: false),
                    authorityid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationoneterritorialrange", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationoneterritorialrange_applicationonebasic_applicat~",
                        column: x => x.applicationonebasicid,
                        principalTable: "applicationonebasic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationoneterritorialrange_authority_authorityid",
                        column: x => x.authorityid,
                        principalTable: "authority",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationonefile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    applicationoneid = table.Column<int>(type: "integer", nullable: true),
                    zopoeshtattachedfileid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationonefile", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationonefile_applicationone_applicationoneid",
                        column: x => x.applicationoneid,
                        principalTable: "applicationone",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicationonefile_zopoeshtattachedfile_zopoeshtattachedfil~",
                        column: x => x.zopoeshtattachedfileid,
                        principalTable: "zopoeshtattachedfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationtwo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationoneid = table.Column<int>(type: "integer", nullable: false),
                    haswaterdamagethreat = table.Column<bool>(type: "boolean", nullable: false),
                    hassoildamagethreat = table.Column<bool>(type: "boolean", nullable: false),
                    hasspeciesdamagethreat = table.Column<bool>(type: "boolean", nullable: false),
                    haswaterdamagedamage = table.Column<bool>(type: "boolean", nullable: false),
                    hassoildamagedamage = table.Column<bool>(type: "boolean", nullable: false),
                    hasspeciesdamagedamage = table.Column<bool>(type: "boolean", nullable: false),
                    threatoccurrencedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    threatconfirmeddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    damageoccurrencedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    damageconfirmeddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    procedureinitiateddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    applicantid = table.Column<int>(type: "integer", nullable: false),
                    legalprocedureresult = table.Column<string>(type: "text", nullable: true),
                    haspreventionprocedureresult = table.Column<bool>(type: "boolean", nullable: false),
                    preventionprocedureresult = table.Column<string>(type: "text", nullable: true),
                    hasremovalprocedureresult = table.Column<bool>(type: "boolean", nullable: false),
                    removalprocedureresult = table.Column<string>(type: "text", nullable: true),
                    casestatus = table.Column<int>(type: "integer", nullable: false),
                    preventionorremovalprocedurefinishdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    preventionorremovalprocedurefinishinformation = table.Column<string>(type: "text", nullable: true),
                    paidbyresponsibleparties = table.Column<decimal>(type: "numeric", nullable: true),
                    recoveredsubsequentlybyresponsibleparties = table.Column<decimal>(type: "numeric", nullable: true),
                    hasunreimbursedexpense = table.Column<bool>(type: "boolean", nullable: false),
                    unreimbursedexpensevalue = table.Column<decimal>(type: "numeric", nullable: true),
                    unreimbursedexpense = table.Column<string>(type: "text", nullable: true),
                    additionalexpensetext = table.Column<string>(type: "text", nullable: true),
                    paymentsource = table.Column<int>(type: "integer", nullable: false),
                    otherpaymentsource = table.Column<string>(type: "text", nullable: true),
                    hasadministrativeexpense = table.Column<bool>(type: "boolean", nullable: false),
                    administrativeexpense = table.Column<decimal>(type: "numeric", nullable: true),
                    additionalinformation = table.Column<string>(type: "text", nullable: true),
                    commitstate = table.Column<int>(type: "integer", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    changestatedescription = table.Column<string>(type: "text", nullable: true),
                    rootid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwo", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwo_applicant_applicantid",
                        column: x => x.applicantid,
                        principalTable: "applicant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwo_applicationone_applicationoneid",
                        column: x => x.applicationoneid,
                        principalTable: "applicationone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationtwocode",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    codeid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwocode", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwocode_applicationtwo_applicationtwoid",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwocode_code_codeid",
                        column: x => x.codeid,
                        principalTable: "code",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationtwofile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: true),
                    zopoeshtattachedfileid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwofile", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwofile_applicationtwo_applicationtwoid",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_applicationtwofile_zopoeshtattachedfile_zopoeshtattachedfil~",
                        column: x => x.zopoeshtattachedfileid,
                        principalTable: "zopoeshtattachedfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationtwosubactivity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    subactivityid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwosubactivity", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwosubactivity_applicationtwo_applicationtwoid",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwosubactivity_subactivity_subactivityid",
                        column: x => x.subactivityid,
                        principalTable: "subactivity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicant_authorityid",
                table: "applicant",
                column: "authorityid");

            migrationBuilder.CreateIndex(
                name: "IX_applicant_individualid",
                table: "applicant",
                column: "individualid");

            migrationBuilder.CreateIndex(
                name: "IX_applicant_legalentityid",
                table: "applicant",
                column: "legalentityid");

            migrationBuilder.CreateIndex(
                name: "IX_applicant_operatorid",
                table: "applicant",
                column: "operatorid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationlock_userid",
                table: "applicationlock",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicantid",
                table: "applicationone",
                column: "applicantid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicationonebasicid",
                table: "applicationone",
                column: "applicationonebasicid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicationonedamageid",
                table: "applicationone",
                column: "applicationonedamageid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicationonelegalentityid",
                table: "applicationone",
                column: "applicationonelegalentityid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicationonethreatid",
                table: "applicationone",
                column: "applicationonethreatid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationonebasic_countryid",
                table: "applicationonebasic",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationonebasic_operatorid",
                table: "applicationonebasic",
                column: "operatorid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationonefile_applicationoneid",
                table: "applicationonefile",
                column: "applicationoneid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationonefile_zopoeshtattachedfileid",
                table: "applicationonefile",
                column: "zopoeshtattachedfileid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneterritorialrange_applicationonebasicid",
                table: "applicationoneterritorialrange",
                column: "applicationonebasicid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneterritorialrange_authorityid",
                table: "applicationoneterritorialrange",
                column: "authorityid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_applicantid",
                table: "applicationtwo",
                column: "applicantid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_applicationoneid",
                table: "applicationtwo",
                column: "applicationoneid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwocode_applicationtwoid",
                table: "applicationtwocode",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwocode_codeid",
                table: "applicationtwocode",
                column: "codeid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwofile_applicationtwoid",
                table: "applicationtwofile",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwofile_zopoeshtattachedfileid",
                table: "applicationtwofile",
                column: "zopoeshtattachedfileid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwosubactivity_applicationtwoid",
                table: "applicationtwosubactivity",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwosubactivity_subactivityid",
                table: "applicationtwosubactivity",
                column: "subactivityid");

            migrationBuilder.CreateIndex(
                name: "IX_email_typeid",
                table: "email",
                column: "typeid");

            migrationBuilder.CreateIndex(
                name: "IX_emailaddressee_emailid",
                table: "emailaddressee",
                column: "emailid");

            migrationBuilder.CreateIndex(
                name: "IX_municipality_districtid",
                table: "municipality",
                column: "districtid");

            migrationBuilder.CreateIndex(
                name: "IX_operator_administrativeunitbasinid",
                table: "operator",
                column: "administrativeunitbasinid");

            migrationBuilder.CreateIndex(
                name: "IX_operator_administrativeunitriosvid",
                table: "operator",
                column: "administrativeunitriosvid");

            migrationBuilder.CreateIndex(
                name: "IX_operator_operatorcorrespondenceid",
                table: "operator",
                column: "operatorcorrespondenceid");

            migrationBuilder.CreateIndex(
                name: "IX_operator_settlementid",
                table: "operator",
                column: "settlementid");

            migrationBuilder.CreateIndex(
                name: "IX_operatorcorrespondence_settlementid",
                table: "operatorcorrespondence",
                column: "settlementid");

            migrationBuilder.CreateIndex(
                name: "IX_passwordtoken_userid",
                table: "passwordtoken",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_settlement_districtid",
                table: "settlement",
                column: "districtid");

            migrationBuilder.CreateIndex(
                name: "IX_settlement_municipalityid",
                table: "settlement",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "IX_subactivity_activityid",
                table: "subactivity",
                column: "activityid");

            migrationBuilder.CreateIndex(
                name: "IX_terrain_districtid",
                table: "terrain",
                column: "districtid");

            migrationBuilder.CreateIndex(
                name: "IX_terrain_municipalityid",
                table: "terrain",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "IX_terrain_operatorid",
                table: "terrain",
                column: "operatorid");

            migrationBuilder.CreateIndex(
                name: "IX_terrain_settlementid",
                table: "terrain",
                column: "settlementid");

            migrationBuilder.CreateIndex(
                name: "IX_user_authorityid",
                table: "user",
                column: "authorityid");

            migrationBuilder.CreateIndex(
                name: "IX_user_roleid",
                table: "user",
                column: "roleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acttype");

            migrationBuilder.DropTable(
                name: "affectedtype");

            migrationBuilder.DropTable(
                name: "applicationhistory");

            migrationBuilder.DropTable(
                name: "applicationlock");

            migrationBuilder.DropTable(
                name: "applicationonefile");

            migrationBuilder.DropTable(
                name: "applicationoneterritorialrange");

            migrationBuilder.DropTable(
                name: "applicationtwocode");

            migrationBuilder.DropTable(
                name: "applicationtwofile");

            migrationBuilder.DropTable(
                name: "applicationtwosubactivity");

            migrationBuilder.DropTable(
                name: "emailaddressee");

            migrationBuilder.DropTable(
                name: "expensetype");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "insurance");

            migrationBuilder.DropTable(
                name: "measuretype");

            migrationBuilder.DropTable(
                name: "passwordtoken");

            migrationBuilder.DropTable(
                name: "section");

            migrationBuilder.DropTable(
                name: "sector");

            migrationBuilder.DropTable(
                name: "terrain");

            migrationBuilder.DropTable(
                name: "code");

            migrationBuilder.DropTable(
                name: "zopoeshtattachedfile");

            migrationBuilder.DropTable(
                name: "applicationtwo");

            migrationBuilder.DropTable(
                name: "subactivity");

            migrationBuilder.DropTable(
                name: "email");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "applicationone");

            migrationBuilder.DropTable(
                name: "activity");

            migrationBuilder.DropTable(
                name: "emailtype");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "applicant");

            migrationBuilder.DropTable(
                name: "applicationonebasic");

            migrationBuilder.DropTable(
                name: "applicationonedamage");

            migrationBuilder.DropTable(
                name: "applicationonelegalentity");

            migrationBuilder.DropTable(
                name: "applicationonethreat");

            migrationBuilder.DropTable(
                name: "authority");

            migrationBuilder.DropTable(
                name: "individual");

            migrationBuilder.DropTable(
                name: "legalentity");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "operator");

            migrationBuilder.DropTable(
                name: "administrativeunit");

            migrationBuilder.DropTable(
                name: "operatorcorrespondence");

            migrationBuilder.DropTable(
                name: "settlement");

            migrationBuilder.DropTable(
                name: "municipality");

            migrationBuilder.DropTable(
                name: "district");
        }
    }
}
