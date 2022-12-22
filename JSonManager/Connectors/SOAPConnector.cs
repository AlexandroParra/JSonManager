using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JSonManager
{
    /// <summary>
    /// Random class
    /// </summary>
    public class SOAPConnector
    {
        /// <summary>
        /// Function that calls a SOAP web service
        /// </summary>
        public void CallSOAP()
        {
            try
            {
                // Construct http post request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://test-submit.health263.systems:8081/apacewebservices/AMF1_0"));
                request.Method = "POST";
                request.ContentType = "application/xml";
                request.Accept = "application/xml";

                // Setting a namespace for your xml
                // I'm not sure the timezone one is set up properly
                XNamespace soap = "urn:apace:member:format:1.0";
                XNamespace timezone = "TimeZone=\"Africa/Johannesburg\"";

                // This constructs your xml using the LINQ library. I modeled after your demo, but needs tweaking as I get 500 error from server.
                XElement requestXML =
                    new XElement(soap + "Member",
                        new XElement("Request",
                            new XElement("Transaction",
                                new XElement("VersionNumber", "1.0"),
                                new XElement("Number", "434252 - 342234 - 6765"),
                                new XElement("SystemIdentifier", "SYSTEM999"),
                                new XElement("DestinationCode", "APACE"),
                                new XElement("ClientCountryCode", "ZA"),
                                new XElement(timezone + "Timestamp", "20160601123456"),
                                new XElement("TestIndicator", "Y"),
                                new XElement("User", "ProviderX/Jane Doe")
                            ),
                            new XElement("MembershipLookup",
                                new XElement("Funder", "AFunder"),
                                new XElement("WithMembershipNumber",
                                    new XElement("MembershipNumber", 123456789)
                                )
                            )
                        )
                    );

                // Convert the xml into a stream that we write to our request
                byte[] bytes = Encoding.UTF8.GetBytes(requestXML.ToString());
                request.ContentLength = bytes.Length;
                using (Stream putStream = request.GetRequestStream())
                {
                    putStream.Write(bytes, 0, bytes.Length);
                }

                // Execute the request and get an xml response "reader". You can read all xml at once or line by line
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {

                    var streamData = reader.ReadToEnd();

                }
            }
            catch (Exception ex)
            {
                // Write exception to console & wait for key press
                Console.WriteLine(ex.Message + ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
