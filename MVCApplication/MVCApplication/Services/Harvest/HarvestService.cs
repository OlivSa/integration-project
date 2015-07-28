using Quartz;
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml.Linq;
using System.Collections.Generic;
using MVCApplication.Models;

namespace MVCApplication.Services.Harvest
{

    public class HarvestService
    {
            private HttpWebResponse response = null;
            private DateTime today = DateTime.Today;
            private string HARVEST_ROOT_URI = "https://yandexru.harvestapp.com";


 //Get Entries
             public IEnumerable<Entry> GetEntries(IEnumerable<int> projectIds)
              {
                var entries = new List<Entry>();
                int daysToStartDate = -5;
                int daysToEndDate = -1;
                var startDate = today.AddDays(daysToStartDate).ToString("yyyyMMdd");
                var endDate = today.AddDays(daysToEndDate).ToString("yyyyMMdd");
                foreach (var projectId in projectIds)
                {
                    string uri = String.Format
                        ("{0}/projects/{1}/entries?from={2}&to={3}&billable=yes&is_closed=yes",
                        HARVEST_ROOT_URI,
                        projectId,
                        startDate,
                        endDate);
                    try
                    {
                        var request = RequestHelper.MakeRequest(uri);
                        using (response = request.GetResponse() as HttpWebResponse)
                        {
                            if (request.HaveResponse == true && response != null)
                            {
                                XDocument doc = RequestHelper.CreateXDocument(response);
                                var elements = doc.Descendants("day-entry");
                                foreach (var entryElement in elements)
                                {
                                    var entry = new Entry();
                                    entry.UserId = Int32.Parse(entryElement.Element("user-id").Value);
                                    entry.ProjectId = Int32.Parse(entryElement.Element("project-id")
                                        .Value);
                                    entry.Hours = Decimal.Parse(entryElement.Element("hours").Value);
                                    entries.Add(entry);
                                }
                            }
                        }
                    }
                    catch (WebException wex)
                    {
                        RequestHelper.CatchWebException(wex);
                    }
                    finally
                    {
                        RequestHelper.CloseResponse(response);

                    }
                }
                return entries;
            }
 // Get projects


             public IEnumerable<Project> GetProjects()
             {

                 int daysToStartDate = -5;
                 var startDate = today.AddDays(daysToStartDate).ToString("yyyy-MM-dd");
                 string uri = String.Format
                        ("{0}/projects?updated_since={1}",
                        HARVEST_ROOT_URI,
                        startDate);
                 var projects = new List<Project>();

                 try
                 {
                     var request = RequestHelper.MakeRequest(uri);
                     using (response = request.GetResponse() as HttpWebResponse)
                     {
                         if (request.HaveResponse == true && response != null)
                         {
                             XDocument doc = RequestHelper.CreateXDocument(response);
                             var elements = doc.Descendants("project");
                             foreach (var entryElement in elements)
                             {
                                 var project = new Project();
                                 project.ClientId = Convert.ToInt32(entryElement.Element("client-id")
                                     .Value);
                                 project.ProjectId = Convert.ToInt32(entryElement.Element("id").Value);
                                 project.Name = entryElement.Element("name").Value;
                                 projects.Add(project);
                             }
                         }
                     }
                 }
                 catch (WebException wex)
                 {
                     RequestHelper.CatchWebException(wex);
                 }
                 finally
                 {
                     RequestHelper.CloseResponse(response);

                 }
                 return projects;
             }

             public IEnumerable<User> GetUsers(IEnumerable<int> userIds)
             {
                 var users = new List<User>();
                 
                 foreach (var userId in userIds)
                 {
                     string uri = String.Format
                        ("{0}/people/{1}",
                        HARVEST_ROOT_URI,
                        userId);
                     try
                     {
                         var request = RequestHelper.MakeRequest(uri);
                         using (response = request.GetResponse() as HttpWebResponse)
                         {
                             if (request.HaveResponse == true && response != null)
                             {
                                 XDocument doc = RequestHelper.CreateXDocument(response);
                                 var elements = doc.Descendants("user");
                                 foreach (var userElement in elements)
                                 {
                                     var user = new User();
                                     user.Id = Convert.ToInt32(userElement.Element("id").Value);
                                     user.FirstName = userElement.Element("first-name").Value;
                                     user.LastName = userElement.Element("last-name").Value;
                                     user.HourlyRate = Decimal.Parse(userElement.Element("cost-rate")
                                         .Value);
                                     users.Add(user);
                                 }
                             }
                         }
                     }
                     catch (WebException wex)
                     {
                         RequestHelper.CatchWebException(wex);
                     }
                     finally
                     {
                         RequestHelper.CloseResponse(response);

                     }
                 }
                     return users;
                     }
//Get Clients
             public IEnumerable<Client> GetClients(IEnumerable<int> clientIds)
             {
                 var clients = new List<Client>();
                 
                 foreach (var clientId in clientIds)
                 {
                     
                     string uri = String.Format
                        ("{0}/clients/{1}",
                        HARVEST_ROOT_URI,
                        clientId);
                     try
                     {
                         var request = RequestHelper.MakeRequest(uri);
                         using (response = request.GetResponse() as HttpWebResponse)
                         {
                             if (request.HaveResponse == true && response != null)
                             {
                                 XDocument doc = RequestHelper.CreateXDocument(response);
                                 var elements = doc.Descendants("client");
                                 foreach (var clientElement in elements)
                                 {
                                     var client = new Client();
                                     client.Id = Convert.ToInt32(clientElement.Element("id").Value);
                                     client.Name = clientElement.Element("name").Value;
                                     client.Currency = clientElement.Element("currency").Value;
                                     client.Details = clientElement.Element("details").Value; ;
                                     clients.Add(client);
                                 }
                             }
                         }
                     }
                     catch (WebException wex)
                     {
                         RequestHelper.CatchWebException(wex);
                     }
                     finally
                     {
                         RequestHelper.CloseResponse(response);

                     }
                 }
                     return clients;
                     }
                 }
             }