# dotnet ef --startup-project ../Zopoesht.Hosting/ migrations add initial --context AppDbContext

dotnet ef --startup-project ../Zopoesht.Hosting/ database update --context AppDbContext

$server = "localhost"
$username = "postgres"
$port = 5432
$db = "zopoesht2"

SET PGCLIENTENCODING=utf-8
chcp 65001

psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/1.District.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/2.Municipality.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/3.Settlement.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/4.Authority.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/5.Role.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/6.User.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/7.Activity.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/8.SubActivity.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/9.Procurature.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/10.MinistryOfInterior.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/11.AdministrativeCourt.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/12.Code.pg.sql"
psql -h $server -U $username -d $db -p $port -f "../Database/InsertScripts/13.Country.pg.sql"
 