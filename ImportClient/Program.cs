
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Data.SqlClient;


class Program
{
    string clientName = "";

    static void Main(string[] args)
    {
        string clientName = "";
        string fileName = @"ClientDatabase_All.xml";

        if (args.Length == 1)
        {
            clientName = args[0];
        }
        else
        {
            Console.WriteLine("you need to specify a client name");
            return; 
        }

        //if (!string.IsNullOrEmpty(clientName))
        //{
        //    fileName = @"ClientDatabase_" + clientName + ".xml";
        //}
        string fileNameAndPath = new Routines.Config().ReadValue("WorkingFolder") + fileName;


        // ignore getting from S3 for the moment some security setting not sure i should tamper with
        if (1 == 2)
        {

            if (System.IO.File.Exists(fileNameAndPath))
            {
                File.Delete(fileNameAndPath);
            }

            Amazon.S3.AmazonS3Client s3Client = new Amazon.S3.AmazonS3Client("AKIAS7VDU44CT5BVUCKW", new Routines.Decrypt().Exec("h7mDbcMs+zyfrKifN7xB9L5eIQ08dPQFs0e01ah+RxPG2StJBIWg9PVKN2TtKX+M")
                , RegionEndpoint.EUWest1);
            try
            {

                TransferUtility transferUtility = new TransferUtility(s3Client);

                TransferUtilityDownloadRequest request = new TransferUtilityDownloadRequest();
                request.BucketName = "earcu-install-files";
                request.Key = "data-migration-files/" + fileName;
                request.FilePath = fileNameAndPath;

                transferUtility.Download(request);
                Console.WriteLine("AWS xfer complete");

            }
            catch (Exception ex)
            {
                Console.WriteLine("AWS xfer error");
                Console.WriteLine(ex.ToString());
            }

        }

        if (!System.IO.File.Exists(fileNameAndPath))
        {
            Console.WriteLine("cant find input file " + fileNameAndPath);
            return;
        }

        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        doc.Load(fileNameAndPath);

        string xpath = "/ClientDatabaseList/ClientDatabase[Name='" + clientName + "']";

        // Select the nodes that match the XPath expression
        XmlNode? clientdatabase = doc.SelectSingleNode(xpath);

        if (clientdatabase == null)
        {
            Console.WriteLine("error finding client " + clientName + " in Xml " + fileNameAndPath);
            return;
        }

        Console.WriteLine("ClientDatbase.Id=" + GetNodeInnerText(clientdatabase, "Id"));
        Console.WriteLine("ClientDatbase.Name=" + GetNodeInnerText(clientdatabase, "Name"));

        Console.WriteLine("ClientDatbase.ConnectionStringDB=" + GetNodeInnerText(clientdatabase, "ConnectionStringDB"));
        Console.WriteLine("ClientDatbase.ConnectionStringLogger=" + GetNodeInnerText(clientdatabase, "ConnectionStringLogger"));
        Console.WriteLine("ClientDatbase.EndPoint=" + GetNodeInnerText(clientdatabase, "EndPoint"));
        Console.WriteLine("ClientDatbase.IsLive=" + GetNodeInnerText(clientdatabase, "IsLive"));
        Console.WriteLine("ClientDatbase.WebServerId=" + GetNodeInnerText(clientdatabase, "WebServerId"));
        Console.WriteLine("ClientDatbase.AppServerId=" + GetNodeInnerText(clientdatabase, "AppServerId"));
        Console.WriteLine("ClientDatbase.SftpServerId=" + GetNodeInnerText(clientdatabase, "SftpServerId"));
        Console.WriteLine("ClientDatbase.DbServerId=" + GetNodeInnerText(clientdatabase, "DbServerId"));
        Console.WriteLine("ClientDatbase.FriendlyName=" + GetNodeInnerText(clientdatabase, "FriendlyName"));
        Console.WriteLine("ClientDatbase.URLName=" + GetNodeInnerText(clientdatabase, "URLName"));
        Console.WriteLine("ClientDatbase.RollOutOffsetDays=" + GetNodeInnerText(clientdatabase, "RollOutOffsetDays"));
        Console.WriteLine("ClientDatbase.CurrentVersion=" + GetNodeInnerText(clientdatabase, "CurrentVersion"));
        Console.WriteLine("ClientDatbase.BuildVersion=" + GetNodeInnerText(clientdatabase, "BuildVersion"));
        Console.WriteLine("ClientDatbase.MojoName=" + GetNodeInnerText(clientdatabase, "MojoName"));
        Console.WriteLine("ClientDatbase.RequiresReleaseNotes=" + GetNodeInnerText(clientdatabase, "RequiresReleaseNotes"));
        Console.WriteLine("ClientDatbase.WebServicesBaseUrl=" + GetNodeInnerText(clientdatabase, "WebServicesBaseUrl"));
        Console.WriteLine("ClientDatbase.ClientDatabaseSLALevelId=" + GetNodeInnerText(clientdatabase, "ClientDatabaseSLALevelId"));
        Console.WriteLine("ClientDatbase.ClientSummaryInfo=" + GetNodeInnerText(clientdatabase, "ClientSummaryInfo"));
        Console.WriteLine("ClientDatbase.ClientPreviousATS=" + GetNodeInnerText(clientdatabase, "ClientPreviousATS"));
        Console.WriteLine("ClientDatbase.ClientSaleStory=" + GetNodeInnerText(clientdatabase, "ClientSaleStory"));
        Console.WriteLine("ClientDatbase.ClientHRsystem=" + GetNodeInnerText(clientdatabase, "ClientHRsystem"));
        Console.WriteLine("ClientDatbase.ClientPrimaryContact=" + GetNodeInnerText(clientdatabase, "ClientPrimaryContact"));
        Console.WriteLine("ClientDatbase.GoLiveDate_Estimated=" + GetNodeInnerText(clientdatabase, "GoLiveDate_Estimated"));
        Console.WriteLine("ClientDatbase.GoLiveDate_Actual=" + GetNodeInnerText(clientdatabase, "GoLiveDate_Actual"));
        Console.WriteLine("ClientDatbase.MonthlyFee=" + GetNodeInnerText(clientdatabase, "MonthlyFee"));
        Console.WriteLine("ClientDatbase.UseSeperateApplicantPortalSkinSolution=" + GetNodeInnerText(clientdatabase, "UseSeperateApplicantPortalSkinSolution"));
        Console.WriteLine("ClientDatbase.SeparateSkinsSolutionName=" + GetNodeInnerText(clientdatabase, "SeparateSkinsSolutionName"));
        Console.WriteLine("ClientDatbase.SeparateSkinsIncludeTestPortal=" + GetNodeInnerText(clientdatabase, "SeparateSkinsIncludeTestPortal"));
        Console.WriteLine("ClientDatbase.PatchingLocked=" + GetNodeInnerText(clientdatabase, "PatchingLocked"));
        Console.WriteLine("ClientDatbase.UnrestrictedStaffAccess=" + GetNodeInnerText(clientdatabase, "UnrestrictedStaffAccess"));
        Console.WriteLine("ClientDatbase.PrimaryContactEmailAddress=" + GetNodeInnerText(clientdatabase, "PrimaryContactEmailAddress"));
        Console.WriteLine("ClientDatbase.SecondaryContactEmailAddress=" + GetNodeInnerText(clientdatabase, "SecondaryContactEmailAddress"));

        Console.WriteLine("ClientDatbase.TertiaryContactEmailAddress=" + GetNodeInnerText(clientdatabase, "TertiaryContactEmailAddress"));
        Console.WriteLine("ClientDatbase.IsSuspended=" + GetNodeInnerText(clientdatabase, "IsSuspended"));
        Console.WriteLine("ClientDatbase.IsTest=" + GetNodeInnerText(clientdatabase, "IsTest"));
        Console.WriteLine("ClientDatbase.IsDemo=" + GetNodeInnerText(clientdatabase, "IsDemo"));
        Console.WriteLine("ClientDatbase.ZendeskOrgId=" + GetNodeInnerText(clientdatabase, "ZendeskOrgId"));
        Console.WriteLine("ClientDatbase.ZendeskOrgName=" + GetNodeInnerText(clientdatabase, "ZendeskOrgName"));
        Console.WriteLine("ClientDatbase.ZendeskOrgIdStr=" + GetNodeInnerText(clientdatabase, "ZendeskOrgIdStr"));

        Console.WriteLine("ClientDatbase.TerminationDate=" + GetNodeInnerText(clientdatabase, "TerminationDate"));
        Console.WriteLine("ClientDatbase.Sector=" + GetNodeInnerText(clientdatabase, "Sector"));
        Console.WriteLine("ClientDatbase.AWS=" + GetNodeInnerText(clientdatabase, "AWS"));
        Console.WriteLine("ClientDatbase.UseSharedBusinessLogic=" + GetNodeInnerText(clientdatabase, "UseSharedBusinessLogic"));
        Console.WriteLine("ClientDatbase.UseSharedAdminPortal=" + GetNodeInnerText(clientdatabase, "UseSharedAdminPortal"));
        Console.WriteLine("ClientDatbase.CurrentBusinessLogicPatch=" + GetNodeInnerText(clientdatabase, "CurrentBusinessLogicPatch"));
        Console.WriteLine("ClientDatbase.CurrentAdminPortalPatch=" + GetNodeInnerText(clientdatabase, "CurrentAdminPortalPatch"));



        XmlNodeList? aliasList = clientdatabase.SelectNodes("AliasList/Alias");

        if (aliasList != null)
        {
            foreach (XmlNode alias in aliasList)
            {
                Console.WriteLine("  Alais.Id=" + GetNodeInnerText(alias, "Id"));
                Console.WriteLine("  Alais.ClientDatabaseId=" + GetNodeInnerText(alias, "ClientDatabaseId"));
                Console.WriteLine("  Alais.Appl_md_path=" + GetNodeInnerText(alias, "Appl_md_path"));
                Console.WriteLine("  Alais.AliasName=" + GetNodeInnerText(alias, "AliasName"));
                Console.WriteLine("  Alais.WebFarmId=" + GetNodeInnerText(alias, "WebFarmId"));
                Console.WriteLine("  Alais.DomainName=" + GetNodeInnerText(alias, "DomainName"));
                Console.WriteLine("  Alais.Url=" + GetNodeInnerText(alias, "Url"));
                Console.WriteLine("  Alais.ProgramId=" + GetNodeInnerText(alias, "ProgramId"));
                Console.WriteLine("  Alais.ProgramTypeId=" + GetNodeInnerText(alias, "ProgramTypeId"));
                Console.WriteLine("  Alais.ClientDatabaseAliasId=" + GetNodeInnerText(alias, "ClientDatabaseAliasId"));
                Console.WriteLine("  Alais.LocalURL=" + GetNodeInnerText(alias, "LocalURL"));
                Console.WriteLine("  Alais.ClientURL=" + GetNodeInnerText(alias, "ClientURL"));

            }
        }


        string query = @"
update clientdatabase set deleted = 0 
, updateddate = getutcdate()
, name = @Name 
where id = @Id
";



        // Use SqlConnection to connect to the database
        using (SqlConnection connection = new SqlConnection(new Routines.Config().ReadValue("AWSConnectionString")))
        {
            // Use SqlCommand to execute the query
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                // Add parameters
                command.Parameters.AddWithValue("@Id", GetNodeInnerText(clientdatabase, "Id"));
                command.Parameters.AddWithValue("@Name", GetNodeInnerText(clientdatabase, "Name"));


                try
                {
                    // Open the connection
                    connection.Open();

                    // Execute the query

                    int rows = command.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        Console.WriteLine("No clientdatabase updated using name = " + clientName);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("Clientdatabase updated using name = " + clientName + " id = " + GetNodeInnerText(clientdatabase, "Id"));

                Console.ReadLine();
            }
        }
    }
    public static string GetNodeInnerText(XmlNode node, string name)
    {
        XmlNode? n = node.SelectSingleNode(name);
        if (n != null)
        {
            return n.InnerText;
        }
        return "";
    }



}