INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('1', 'Administrator', 'ADMINISTRATOR');

INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('2', 'Customer', 'CUSTOMER');

INSERT INTO AspNetUserRoles (RoleId, UserId) (SELECT '1', Id FROM AspNetUsers);