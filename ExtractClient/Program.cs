
using System;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;


class Program
{
    string clientName = "";
    
    static void Main(string[] args)
    {
        string clientName = "";

        if (args.Length > 0)
        {
            clientName = args[0];
        }
         
    
        // SQL query to look up clientName in a table
        //string query = "SELECT * FROM Clientdatabase WHERE Name = @ClientName";
        string query = @" 




SELECT (
    SELECT ClientDatabase.* , 
	(
		SELECT 
            Alias.*
        FROM 
            Alias 
        WHERE 
            Alias.ClientDatabaseId = ClientDatabase.ID
        FOR XML PATH('Alias'), TYPE
    ) as 'AliasList'
	
    FROM ClientDatabase ";
        if (!String.IsNullOrWhiteSpace(clientName))
        {
            query += "WHERE name = '@clientName'";
        }
        query += @"     
    FOR XML AUTO, ROOT('ClientDatabaseList'), ELEMENTS
) as xmlData

";
        // Use SqlConnection to connect to the database
        using (SqlConnection connection = new SqlConnection(Settings.Env.RackSpaceConnectionString))
        {
            // Use SqlCommand to execute the query
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (!String.IsNullOrWhiteSpace(clientName))
                {
                    // Add parameter for ClientName
                    command.Parameters.AddWithValue("@clientName", clientName);
                }

                try
                {
                    // Open the connection
                    connection.Open();

                    // Execute the query and read the results
                    SqlDataReader reader = command.ExecuteReader();
                    int recordCount = 0;
                    while (reader.Read()) // only expecting 1 record 
                    {
                        if (recordCount++ == 0)
                        {
                            string xmlData = reader["xmlData"] + "";

                            string outputFileName = @"ClientDatabase_All.xml";
                            if (!string.IsNullOrEmpty(clientName))
                            {
                                outputFileName = @"ClientDatabase_" + clientName + ".xml";
                            }

                            if (System.IO.File.Exists(Settings.Env.OutputFolder + outputFileName))
                            {
                                File.Delete(Settings.Env.OutputFolder + outputFileName);
                            }

                            File.WriteAllText(Settings.Env.OutputFolder + outputFileName, xmlData);

                            Console.WriteLine(Settings.Env.OutputFolder + outputFileName + " Created");


                            Amazon.S3.AmazonS3Client s3Client = new Amazon.S3.AmazonS3Client("AKIAS7VDU44CT5BVUCKW", new Routines.Decrypt().Exec("h7mDbcMs+zyfrKifN7xB9L5eIQ08dPQFs0e01ah+RxPG2StJBIWg9PVKN2TtKX+M")
                                , RegionEndpoint.EUWest1);
                            try
                            {

                                TransferUtility transferUtility = new TransferUtility(s3Client);

                                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                                
                                request.BucketName = "earcu-install-files";
                                request.FilePath = Settings.Env.OutputFolder +  outputFileName;
                                request.Key = "data-migration-files/" + outputFileName;
                                request.ContentType = "text/xml";
                                request.CannedACL = S3CannedACL.BucketOwnerFullControl;
                                request.ServerSideEncryptionMethod = new ServerSideEncryptionMethod("AES256");

                                transferUtility.Upload(request);
                                Console.WriteLine("AWS xfer complete");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("AWS xfer error");
                                Console.WriteLine(ex.ToString());
                            }

                        }
                        
                        if (recordCount == 0)
                        {
                            Console.WriteLine("No client found using name=" + clientName);
                        }
                        if (recordCount > 1)
                        {
                            Console.WriteLine("Duplicate clients found using name=" + clientName);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }

        Console.ReadLine();
    }

     



}