using DCM.Core.ValueObjects;
using System.Xml.Linq;

namespace DCM.Application.Services.Implementations
{
    public class UnattendXmlGenerator
    {
        public string GenerateXml(UnattendConfig config)
        {
            var ns = "urn:schemas-microsoft-com:unattend";
            var doc = new XDocument(
                new XElement("unattend",
                    new XAttribute(XNamespace.Xmlns + "wcm", "http://schemas.microsoft.com/WMIConfig/2002/State"),
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute("xmlns", ns),

                    new XElement("settings", new XAttribute("pass", "specialize"),
                        new XElement("component",
                            new XAttribute("name", "Microsoft-Windows-Shell-Setup"),
                            new XAttribute("processorArchitecture", "amd64"),
                            new XAttribute("publicKeyToken", "31bf3856ad364e35"),
                            new XAttribute("language", "neutral"),
                            new XAttribute("versionScope", "nonSxS"),
                            new XElement("ComputerName", config.ComputerName),
                            new XElement("TimeZone", config.TimeZone)
                        ),
                        config.JoinDomain ?
                            new XElement("component",
                                new XAttribute("name", "Microsoft-Windows-UnattendedJoin"),
                                new XAttribute("processorArchitecture", "amd64"),
                                new XAttribute("publicKeyToken", "31bf3856ad364e35"),
                                new XAttribute("language", "neutral"),
                                new XAttribute("versionScope", "nonSxS"),
                                new XElement("Identification",
                                    new XElement("JoinDomain", config.DomainName),
                                    new XElement("MachineObjectOU", config.OUPath),
                                    new XElement("Credentials",
                                        new XElement("Domain", config.DomainName),
                                        new XElement("Username", config.DomainUser),
                                        new XElement("Password", config.DomainPassword)
                                    )
                                )
                            )
                        : null
                    ),

                    new XElement("settings", new XAttribute("pass", "oobeSystem"),
                        new XElement("component",
                            new XAttribute("name", "Microsoft-Windows-Shell-Setup"),
                            new XAttribute("processorArchitecture", "amd64"),
                            new XAttribute("publicKeyToken", "31bf3856ad364e35"),
                            new XAttribute("language", "neutral"),
                            new XAttribute("versionScope", "nonSxS"),
                            new XElement("UserAccounts",
                                new XElement("AdministratorPassword",
                                    new XElement("Value", config.LocalAdminPassword),
                                    new XElement("PlainText", "true")
                                )
                            ),
                            new XElement("OOBE",
                                new XElement("HideEULAPage", "true"),
                                new XElement("NetworkLocation", "Work"),
                                new XElement("ProtectYourPC", "3")
                            )
                        )
                    )
                )
            );

            return doc.Declaration + "\n" + doc.ToString();
        }
    }

}
