using BackEndProject.Models;
using ClientApi.Models;

namespace ClientApi.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Playgrounds.Any())
                {
                    context.Playgrounds.AddRange(new List<Playground>()
                    {
                        new Playground
                        {
                            Name = "Estadio Santiago Bernabéu",
                            Destination = "Av. de Concha Espina, 1, 28036 Madrid",
                            CordinatesX = "40.453053",
                            CordinatesY = "-3.688344",
                            Capacity = 10,
                        },
                        new Playground
                        {
                            Name = "Alianz Arena",
                            Destination = "Werner-Heisenberg-Allee 25, 80939 München",
                            CordinatesX = "48.218967",
                            CordinatesY = "11.623746",
                            Capacity = 8,
                        },
                        new Playground
                        {
                            Name = "Parc des Princes",
                            Destination = "24 Rue du Commandant Guilbaud, 75016 Paris",
                            CordinatesX = "48.841465",
                            CordinatesY = "2.252616",
                            Capacity = 12,
                        },
                        new Playground
                        {
                            Name = "Camp Nou",
                            Destination = "C. d'Arístides Maillol, 12, 08028 Barcelona",
                            CordinatesX = "41.380898",
                            CordinatesY = "2.122820",
                            Capacity = 10,
                        }
                    });
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User
                        {
                            Name = "test2",
                            Email = "test2@gmail.com",
                            Age = 23,
                            City = "Madryt"
                        }
                    });
                    context.SaveChanges();
                }

            }
        }   
    }
}
