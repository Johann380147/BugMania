namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using BugMania.Shapes;


    internal sealed class Configuration : DbMigrationsConfiguration<BugMania.DataContexts.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\Migrations";
        }

        protected override void Seed(BugMania.DataContexts.ApplicationDbContext context)
        {
            try
            {
                // Tags
                //IList<Tag> defaultTags = new List<Tag>();

                //defaultTags.Add(new Tag() { Name = "C#", Description = "" });
                //defaultTags.Add(new Tag() { Name = "ASP.net", Description = "" });
                //defaultTags.Add(new Tag() { Name = "Wicked", Description = "" });

                //context.Tags.AddOrUpdate(defaultTags.Cast<Tag>().ToArray());

                //// Status
                //IList<Status> defaultStatus = new List<Status>();

                //defaultStatus.Add(new Status() { Name = "NEW", Description = "" });
                //defaultStatus.Add(new Status() { Name = "ASSIGNED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "UNVERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "VERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "INVALID", Description = "" });
                //defaultStatus.Add(new Status() { Name = "DUPLICATE", Description = "" });

                //context.Status.AddOrUpdate(defaultStatus.Cast<Status>().ToArray());

                //// Severity
                //IList<Severity> defaultSeverity = new List<Severity>();

                //defaultSeverity.Add(new Severity() { Name = "Low", Description = "" });
                //defaultSeverity.Add(new Severity() { Name = "Minor", Description = "" });
                //defaultSeverity.Add(new Severity() { Name = "Major", Description = "" });
                //defaultSeverity.Add(new Severity() { Name = "Critical", Description = "" });

                //context.Severities.AddOrUpdate(defaultSeverity.Cast<Severity>().ToArray());



                //// Product
                //IList<Product> defaultProduct = new List<Product>();

                //defaultProduct.Add(new Product() { Name = "Mountain Dew", Description = "" });
                //defaultProduct.Add(new Product() { Name = "Bed Sheet", Description = "" });
                //defaultProduct.Add(new Product() { Name = "Cushion Relaxer 3000", Description = "" });
                //defaultProduct.Add(new Product() { Name = "Tissue Paper", Description = "" });

                //context.Products.AddOrUpdate(defaultProduct.Cast<Product>().ToArray());

                //// Priority
                //IList<Priority> defaultPriority = new List<Priority>();

                //defaultPriority.Add(new Priority() { Name = "Low", Description = "" });
                //defaultPriority.Add(new Priority() { Name = "Medium", Description = "" });
                //defaultPriority.Add(new Priority() { Name = "High", Description = "" });
                //defaultPriority.Add(new Priority() { Name = "Immediate", Description = "" });

                //context.Priorities.AddOrUpdate(defaultPriority.Cast<Priority>().ToArray());

                // ApplicationUsers
                //IList<ApplicationUsers> defaultUsers = new List<ApplicationUsers>();

                //defaultUsers.Add(new Status() { Name = "NEW", Description = "" });

                //context.ApplicationUsers;.AddOrUpdate(defaultUsers.Cast<ApplicationUsers>().ToArray());

                // Group
                //IList<Group> defaultGroup = new List<Group>();

                //defaultGroup.Add(new Group() { Name = "Razor" });
                //defaultGroup.Add(new Group() { Name = "Cloud Nine" });
                //defaultGroup.Add(new Group() { Name = "Bambie" });

                //context.Groups.AddOrUpdate(defaultGroup.Cast<Group>().ToArray());

                //GroupMember
                //IList<GroupMember> defaultGroupMembers = new List<GroupMember>();

                //defaultGroupMembers.Add(new GroupMember() {  });
                //defaultGroupMembers.Add(new GroupMember() { Name = "ASSIGNED", Description = "" });
                //defaultGroupMembers.Add(new GroupMember() { Name = "UNVERIFIED_FIXED", Description = "" });
                //defaultGroupMembers.Add(new GroupMember() { Name = "VERIFIED_FIXED", Description = "" });
                //defaultGroupMembers.Add(new GroupMember() { Name = "INVALID", Description = "" });
                //defaultGroupMembers.Add(new GroupMember() { Name = "DUPLICATE", Description = "" });

                //context.GroupMembers.AddOrUpdate(defaultGroupMembers.Cast<GroupMember>().ToArray());

                //BugReport
                //IList<Status> defaultStatus = new List<Status>();

                //defaultStatus.Add(new Status() { Name = "NEW", Description = "" });
                //defaultStatus.Add(new Status() { Name = "ASSIGNED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "UNVERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "VERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "INVALID", Description = "" });
                //defaultStatus.Add(new Status() { Name = "DUPLICATE", Description = "" });

                //context.Status.AddOrUpdate(defaultTags.Cast<Status>().ToArray());

                // Comment
                //IList<Status> defaultStatus = new List<Status>();

                //defaultStatus.Add(new Status() { Name = "NEW", Description = "" });
                //defaultStatus.Add(new Status() { Name = "ASSIGNED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "UNVERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "VERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "INVALID", Description = "" });
                //defaultStatus.Add(new Status() { Name = "DUPLICATE", Description = "" });

                //context.Status.AddOrUpdate(defaultTags.Cast<Status>().ToArray());



                // Role
                //IList<Status> defaultStatus = new List<Status>();

                //defaultStatus.Add(new Status() { Name = "NEW", Description = "" });
                //defaultStatus.Add(new Status() { Name = "ASSIGNED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "UNVERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "VERIFIED_FIXED", Description = "" });
                //defaultStatus.Add(new Status() { Name = "INVALID", Description = "" });
                //defaultStatus.Add(new Status() { Name = "DUPLICATE", Description = "" });

                //context.Status.AddOrUpdate(defaultTags.Cast<Status>().ToArray());


                base.Seed(context);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {
                        // get the error message 
                        string errorMessage = validationError.ErrorMessage;
                        System.Diagnostics.Debug.WriteLine(errorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
