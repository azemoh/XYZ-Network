using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApp.Models;
using System.Security.Claims;

namespace WebApp.DAL {

    public class WebAppDbInitializer : CreateDatabaseIfNotExists<WebAppDbContext> {

        protected override void Seed(WebAppDbContext context) {
            //base.Seed(context);

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            // Create Roles
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Student" });
            context.SaveChanges();

            #region Default Users

            var users = new List<RegisterViewModel> {
                new RegisterViewModel {
                    FirstName = "Joshua",
                    LastName = "Azemoh",
                    Sex = "Male",
                    Password = "passW0rd!",
                    Email = "admin@asp.net",
                    AccountType = "Admin"
                },
                new RegisterViewModel {
                    FirstName = "Samuel",
                    LastName = "Ajayi",
                    Sex = "Male",
                    Password = "passW0rd!",
                    Email = "sammy@asp.net",
                    AccountType = "Student"
                },
                new RegisterViewModel {
                    FirstName = "Angela",
                    LastName = "Olawale",
                    Sex = "Female",
                    Password = "passW0rd!",
                    Email = "angela@asp.net",
                    AccountType = "Student"
                },
                new RegisterViewModel {
                    FirstName = "Felix",
                    LastName = "Nosa",
                    Sex = "Male",
                    Password = "passW0rd!",
                    Email = "felix@asp.net",
                    AccountType = "Student"
                },
                new RegisterViewModel {
                    FirstName = "Grace",
                    LastName = "Okoro",
                    Sex = "Female",
                    Password = "passW0rd!",
                    Email = "grace@asp.net",
                    AccountType = "Student"
                }
            };

            foreach(RegisterViewModel model in users) {
                if(!context.Users.Any(u => u.UserName == model.Email)) {
                    var user = new User {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Sex = model.Sex,
                        Email = model.Email,
                        UserName = model.Email,
                        AccountType = model.AccountType
                    };
                    IdentityResult result = userManager.Create(user, model.Password);
                    if(result.Succeeded) {
                        userManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, model.FirstName));
                        userManager.AddToRole(user.Id, user.AccountType);
                        if(user.AccountType == "Student") {
                            var service = new StudentService(context);
                            // Add Student
                            service.AddStudent(user.Id);
                        }

                    }
                }
            }

            context.SaveChanges();


            #endregion


            #region Default_Tests

            var test = new List<Test> {
				new Test {
					Title = "CSS",
					Questions = new List<TestQuestion> {
                        new TestQuestion {
                            Code = "",
                            Question = "Are CSS property names case-sensitive?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = false },
                                new TestOption { Option = "No", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "Does setting margin-top and margin-bottom have an affect on an inline element?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = false },
                                new TestOption { Option = "No", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "Does setting padding-top and padding-bottom on an inline element add to its dimensions?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = false },
                                new TestOption { Option = "No", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "The pseudo class :checked will select inputs with type radio or checkbox, but not option elements.",
                            Options = new List<TestOption> {
                                new TestOption { Option = "True", IsCorrect = false },
                                new TestOption { Option = "False", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "In a HTML document, the pseudo class :root always refers to the <html> element.",
                            Options = new List<TestOption> {
                                new TestOption { Option = "True", IsCorrect = true },
                                new TestOption { Option = "False", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "<p id=\"example\">Hello</p>\n<style>#example { margin-bottom: -5px; }</style>",
                            Question = "What will happen to the position of #example?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "It will move 5px downwards", IsCorrect = false },
                                new TestOption { Option = "Elements succeeding #example with move 5px upwards", IsCorrect = true },
                                new TestOption { Option = "Neither", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "<p id=\"example\">Hello</p>\n<style>#example { margin-left: -5px; }</style>",
                            Question = "What will happen to the position of #example?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "It will move 5px left", IsCorrect = true },
                                new TestOption { Option = "All elements preceding #example with move 5px to the right", IsCorrect = false },
                                new TestOption { Option = "Neither", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "<ul class=\"shopping-list\" id=\"awesome\">\n<li><span>Milk</span></li>\n<li class=\"favorite\" id=\"must-buy\"><span class=\"highlight\">Sausage</span></li>\n</ul>\n<style>\nul { color: red; }\nli { color: blue; }\n</style>",
                            Question = "What is the color of the text Sausage ?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Red", IsCorrect = false },
                                new TestOption { Option = "Blue", IsCorrect = true },
                                new TestOption { Option = "Neither", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "<ul class=\"shopping-list\" id=\"awesome\">\n<li><span>Milk</span></li>\n<li class=\"favorite\" id=\"must-buy\"><span class=\"highlight\">Sausage</span></li>\n</ul>\n<style>\n.shopping-list .favorite { color: red; }\n#must-buy { color: blue; }\n</style>",
                            Question = "What is the color of the text Sausage ?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Red", IsCorrect = false },
                                new TestOption { Option = "Blue", IsCorrect = true },
                                new TestOption { Option = "Neither", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "@media only screen and (max-width: 1024px)",
                            Question = "Does the screen keyword apply to the device's physical screen or the browser's viewport?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Device's physical screen", IsCorrect = false },
                                new TestOption { Option = "Browser's viewport", IsCorrect = true }
                            }
                        }
					}
				},

                new Test {
                    Title = "HTML",
                    Questions = new List<TestQuestion> {
                        new TestQuestion {
                            Code = "",
                            Question = "Is keygen a valid HTML5 element?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = true },
                                new TestOption { Option = "No", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "bdo tag change the direction of text",
                            Options = new List<TestOption> {
                                new TestOption { Option = "false", IsCorrect = false },
                                new TestOption { Option = "True", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "<figure>\n<img src=\"myimage.jpg\" alt=\"My image\">\n<figcaption>This is my self portrait.</figcaption>\n</figure>",
                            Question = "Is the above HTML valid?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = true },
                                new TestOption { Option = "No", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "If a web page contains organic, multiple h1 tags, will it affect the SEO negativley?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = false },
                                new TestOption { Option = "No", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "If you have a page of search results and want to highlight the search term, what HTML tag would you use?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "strong", IsCorrect = false },
                                new TestOption { Option = "mark", IsCorrect = true },
                                new TestOption { Option = "em", IsCorrect = false },
                                new TestOption { Option = "highlight", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "Does HTML5 support block-level links?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = true },
                                new TestOption { Option = "No", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "<img src=\"mypic.jpg\" style=\"visibility: hidden\" alt=\"My picture\">",
                            Question = "Does the HTML above trigger a http request when the page first loads ?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = true },
                                new TestOption { Option = "No", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "<link href=\"style.css\" rel=\"stylesheet\">\n<script> alert(\"Hello World\"); </script>",
                            Question = "Does style.css have to be downloaded and parsed before Hello World is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = true },
                                new TestOption { Option = "No", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "<link href=\"style1.css\" rel=\"stylesheet\">\n<link href=\"style2.css\" rel=\"stylesheet\">",
                            Question = "Does style1.css have to be downloaded and parsed before style2.css can be fetched? ",
                            Options = new List<TestOption> {
                                new TestOption { Option = "Yes", IsCorrect = false },
                                new TestOption { Option = "No", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "",
                            Question = "Which tag is not supported in HTML5?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "map", IsCorrect = false },
                                new TestOption { Option = "frameset", IsCorrect = true },
                                new TestOption { Option = "area", IsCorrect = false },
                                new TestOption { Option = "embed", IsCorrect = false }
                            }
                        }
                    }
                },

                new Test {
                    Title = "JavaScript",
                    Questions = new List<TestQuestion> {
                        new TestQuestion {
                            Code = "\"1\" + 2 + \"3\" + 4",
                            Question = "What does the above statement evaluate to?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "10", IsCorrect = false },
                                new TestOption { Option = "1234", IsCorrect = true },
                                new TestOption { Option = "37", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "4 + 3 + 2 + \"1\"",
                            Question = "What does the above statement evaluate to?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "10", IsCorrect = false },
                                new TestOption { Option = "4321", IsCorrect = false },
                                new TestOption { Option = "91", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "\"1\" + 2 + \"3\" + 4",
                            Question = "What does the above statement evaluate to?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "10", IsCorrect = false },
                                new TestOption { Option = "1234", IsCorrect = true },
                                new TestOption { Option = "37", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "var foo = 1;\nfunction bar() {\n   foo = 10;\n    return;\n    function foo() {}\n}\nbar();\nalert(foo);",
                            Question = "What is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "1", IsCorrect = true },
                                new TestOption { Option = "10", IsCorrect = false },
                                new TestOption { Option = "undefined", IsCorrect = false },
                                new TestOption { Option = "Error", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "function bar() {\nreturn foo;\nfoo = 10;\nfunction foo() {}\nvar foo = 11;\n}\nalert(typeof bar());",
                            Question = "What is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "number", IsCorrect = false },
                                new TestOption { Option = "function", IsCorrect = true },
                                new TestOption { Option = "undefined", IsCorrect = false },
                                new TestOption { Option = "Error", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "x = 1;\nfunction bar() {\nthis.x = 2;\nreturn x;\n}\nvar foo = new bar();\nalert(foo.x);",
                            Question = "What value is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "1", IsCorrect = false },
                                new TestOption { Option = "2", IsCorrect = true },
                                new TestOption { Option = "undefined", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "function foo(a) {\nalert(arguments.length);\n}\nfoo(1, 2, 3);",
                            Question = "What value is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "1", IsCorrect = false },
                                new TestOption { Option = "2", IsCorrect = false },
                                new TestOption { Option = "3", IsCorrect = true },
                                new TestOption { Option = "4", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "var foo = function bar() {};\nalert(typeof bar);",
                            Question = "What value is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "function", IsCorrect = false },
                                new TestOption { Option = "object", IsCorrect = false },
                                new TestOption { Option = "undefined", IsCorrect = true }
                            }
                        },
                        new TestQuestion {
                            Code = "var arr = [];\narr[0]  = 'a';\narr[1]  = 'b';\narr.foo = 'c';\nalert(arr.length);",
                            Question = "What value is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "1", IsCorrect = false },
                                new TestOption { Option = "2", IsCorrect = true },
                                new TestOption { Option = "3", IsCorrect = false },
                                new TestOption { Option = "undefined", IsCorrect = false }
                            }
                        },
                        new TestQuestion {
                            Code = "function foo(){};\ndelete foo.length;\nalert(typeof foo.length);",
                            Question = "What value is alerted?",
                            Options = new List<TestOption> {
                                new TestOption { Option = "number", IsCorrect = true },
                                new TestOption { Option = "undefined", IsCorrect = false },
                                new TestOption { Option = "object", IsCorrect = false },
                                new TestOption { Option = "Error", IsCorrect = false }
                            }
                        }
                    }
                }
			};

            #endregion

            test.ForEach(t => context.Tests.AddOrUpdate(i => i.Title, t));
            context.SaveChanges();

            var testScores = new List<TestScore> {
                new TestScore { StudentId = 1, TestId = 2, Score = 80 },
                new TestScore { StudentId = 2, TestId = 1, Score = 90 },
                new TestScore { StudentId = 1, TestId = 3, Score = 70 },
                new TestScore { StudentId = 3, TestId = 2, Score = 80 },
                new TestScore { StudentId = 3, TestId = 1, Score = 70 },
            };

            testScores.ForEach(t => context.TestScores.Add(t));
            context.SaveChanges();
        }

    }
}