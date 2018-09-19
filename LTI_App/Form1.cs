using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;
using LTI_App.Networks_OPS;
using LTI_App.UsageReport_OPS;
using LTI_App.Instances_OPS;

namespace LTI_App
{
    public partial class LTI_APP : Form
    {

        string baseURIgets;
        string baseURIdeletes;





        public LTI_APP()
        {

            InitializeComponent();


        }


        #region Get_methods_ODL
        //Não tirem estes #region e #endregion, são umas cenas que eu meti só para organizar o código para mim. São regiões onde só meto métodos daquele tipo

        private List<Node> GetNodes(string ip, string port, string username, string password)
        {
            baseURIgets = @"http://" + ip + ":" + port + "/restconf/operational/opendaylight-inventory:nodes";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURIgets);

            //Authorization para podermos fazer os requests

            string cre = String.Format("{0}:{1}", username, password);

            byte[] bytes = Encoding.ASCII.GetBytes(cre);

            string base64 = Convert.ToBase64String(bytes);

            request.Headers.Add("Authorization", "basic " + base64);

            //acaba aqui a parcela da authorization



            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                MessageBox.Show(response.StatusCode.ToString());

            }

            string content = string.Empty;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader srReader = new StreamReader(stream))
                {
                    content = srReader.ReadToEnd();
                }

            }

            List<Node> nodes = DeserializeNodes.FromJson(content).NodesList.Nodes;

            foreach (Node node in nodes)
            {
                comboBoxNode.Items.Add(node.Id);
                richTextBox2.AppendText("Node: " + node.Id + Environment.NewLine + Environment.NewLine);
            }

            //Como mete os dados na RichTextBox1
            richTextBox1.Clear();
            richTextBox1.Text = response.StatusCode.ToString();
            //richTextBox2.Text = JsonConvert.DeserializeObject<dynamic>(content).ToString();
            return nodes;
        }

        private List<FlowNodeInventoryTable> GetSpecificNode(string ip, string port, string username, string password, string node)
        {
            comboBoxNode.Items.Clear();
            var nodes = GetNodes(ip, port, username, password);
            richTextBox2.Clear();
            comboBoxNode.Refresh();
            baseURIgets = @"http://" + ip + ":" + port + "/restconf/operational/opendaylight-inventory:nodes";

            string newURI;

            newURI = baseURIgets + "/node/" + node;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newURI);

            string cre = String.Format("{0}:{1}", username, password);

            byte[] bytes = Encoding.ASCII.GetBytes(cre);

            string base64 = Convert.ToBase64String(bytes);

            request.Headers.Add("Authorization", "basic " + base64);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                MessageBox.Show(response.StatusCode.ToString());

            }

            string content = string.Empty;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader srReader = new StreamReader(stream))
                {
                    content = srReader.ReadToEnd();
                }

            }

            List<FlowNodeInventoryTable> tables = nodes.ToList().Find(x => x.Id == node).FlowNodeInventoryTable;

            richTextBox2.AppendText("Tables:" + Environment.NewLine);
            foreach (FlowNodeInventoryTable table in tables)
            {
                comboBoxTable.Items.Add(table.Id);
                richTextBox2.AppendText(Environment.NewLine + "id: " + table.Id + Environment.NewLine + "Active Flows:" + table.OpendaylightFlowTableStatisticsFlowTableStatistics.ActiveFlows
                    + Environment.NewLine);
            }



            //Como mete os dados na RichTextBox1
            richTextBox1.Clear();
            richTextBox1.Text = response.StatusCode.ToString();
            //richTextBox2.Text = JsonConvert.DeserializeObject<dynamic>(content).ToString();

            return tables;

        }

        private List<Flow> GetSpecificTable(string ip, string port, string username, string password, string node, string table)
        {
            comboBoxNode.Items.Clear();
            var nodes = GetNodes(ip, port, username, password);
            richTextBox2.Clear();
            comboBoxTable.Items.Clear();
            var tables = GetSpecificNode(ip, port, username, password, node);
            richTextBox2.Clear();
            comboBoxNode.Refresh();
            comboBoxTable.Refresh();

            baseURIgets = @"http://" + ip + ":" + port + "/restconf/operational/opendaylight-inventory:nodes";
            string newURI;
            newURI = baseURIgets + "/node/" + node + "/table/" + table;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newURI);

            //Authorization para podermos fazer os requests

            string cre = String.Format("{0}:{1}", username, password);

            byte[] bytes = Encoding.ASCII.GetBytes(cre);

            string base64 = Convert.ToBase64String(bytes);

            request.Headers.Add("Authorization", "basic " + base64);

            //acaba aqui a parcela da authorization

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                MessageBox.Show(response.StatusCode.ToString());

            }

            string content = string.Empty;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader srReader = new StreamReader(stream))
                {
                    content = srReader.ReadToEnd();
                }

            }

            List<Flow> flows = tables.ToList().Find(x => x.Id == table).Flow;
            richTextBox2.Clear();
            richTextBox2.AppendText("Flows:" + Environment.NewLine);

            foreach (Flow flow in flows)
            {
                comboBoxFlow.Items.Add(flow.Id);
                richTextBox2.AppendText(Environment.NewLine + "ID: " + flow.Id + Environment.NewLine + "Priority: " + flow.Priority + Environment.NewLine);
            }

            //Como mete os dados na RichTextBox1
            richTextBox1.Clear();
            richTextBox1.Text = response.StatusCode.ToString();
            //richTextBox2.Text = JsonConvert.DeserializeObject<dynamic>(content).ToString();
            return flows;
        }

        private void GetSpecificFlow(string ip, string port, string username, string password, string node, string table, string flow)
        {
            comboBoxNode.Items.Clear();
            var nodes = GetNodes(ip, port, username, password);
            richTextBox2.Clear();
            comboBoxTable.Items.Clear();
            var tables = GetSpecificNode(ip, port, username, password, node);
            richTextBox2.Clear();
            comboBoxFlow.Items.Clear();
            var flows = GetSpecificTable(ip, port, username, password, node, table);
            richTextBox2.Clear();
            comboBoxNode.Refresh();
            comboBoxTable.Refresh();
            comboBoxFlow.Refresh();
            baseURIgets = @"http://" + ip + ":" + port + "/restconf/operational/opendaylight-inventory:nodes";
            string newURI;
            newURI = baseURIgets + "/node/" + node + "/table/" + table + "/flow/" + flow;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newURI);

            //Authorization para podermos fazer os requests

            string cre = String.Format("{0}:{1}", username, password);

            byte[] bytes = Encoding.ASCII.GetBytes(cre);

            string base64 = Convert.ToBase64String(bytes);

            request.Headers.Add("Authorization", "basic " + base64);

            //acaba aqui a parcela da authorization

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                MessageBox.Show(response.StatusCode.ToString());

            }


            string content = string.Empty;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader srReader = new StreamReader(stream))
                {
                    content = srReader.ReadToEnd();
                }

            }

            Flow selectedFlow = flows.ToList().Find(x => x.Id == flow);

            richTextBox2.AppendText("ID: " + selectedFlow.Id + Environment.NewLine + "Priority: " + selectedFlow.Priority + Environment.NewLine);
            //Como mete os dados na RichTextBox2

            richTextBox1.Text = response.StatusCode.ToString();
            //richTextBox2.Text = JsonConvert.DeserializeObject<dynamic>(content).ToString();
        }





        #endregion

        #region create flow ODL

        private void CreateFlowDrop(string ip, string port, string username, string password, string node, string table, string newFlowId, string priority)
        {

            var client = new RestClient("http://" + ip + ":" + port + "/restconf/config/opendaylight-inventory:nodes/node/" + node + "/table/" + table + "/flow/" + newFlowId + "");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Postman-Token", "8a697fb9-ad13-4101-9d0d-6ca4a95e47ae");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password)));
            //request.AddHeader("Authorization", "Basic YWRtaW46YWRtaW4=");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n    \"flow\": [\r\n        {\r\n            \"table_id\": \"" + table + "\",\r\n            \"id\": \"" + newFlowId + "\",\r\n            \"priority\": \"" + priority + "\",\r\n            \"match\": {\r\n                \"in-port\": \"" + node + ":1\"\r\n            },\r\n            \"instructions\": {\r\n                \"instruction\": [\r\n                    {\r\n                        \"order\": 0,\r\n                        \"apply-actions\": {\r\n                            \"action\": [\r\n                                {\r\n                                    \"order\": 0,\r\n                                    \"drop-action\": {}\r\n                                }\r\n                            ]\r\n                        }\r\n                    }\r\n                ]\r\n            }\r\n        }\r\n    ]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var sResponse = response.Content;
            richTextBox1.Clear();
            richTextBox1.Text = response.StatusCode.ToString();
            //richTextBox2.Text = JsonConvert.DeserializeObject<dynamic>(sResponse).ToString();
        }

        private void CreateFlow(string ip, string port, string username, string password, string node, string table, string newFlowId, string priority)
        {
            var client = new RestClient("http://" + ip + ":" + port + "/restconf/config/opendaylight-inventory:nodes/node/" + node + "/table/" + table + "/flow/" + newFlowId + "");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Postman-Token", "e4edac10-99d2-44d5-9806-6c9551c59ff9");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password)));
            //request.AddHeader("Authorization", "Basic YWRtaW46YWRtaW4=");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n    \"flow\": [\r\n        {\r\n            \"table_id\": \"" + table + "\",\r\n            \"id\": \"" + newFlowId + "\",\r\n            \"priority\": \"" + priority + "\"\r\n            \r\n        }\r\n    ]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var sResponse = response.Content;
            richTextBox1.Clear();
            richTextBox1.Text = response.StatusCode.ToString();
            //richTextBox2.Text = JsonConvert.DeserializeObject<dynamic>(sResponse).ToString();

        }


        #endregion

        #region Delete Methods OpenDayLight

        private string deleteSpecificFlow(string ip, string port, string username, string password, string node, string table, string flow)
        {
            baseURIdeletes = @"http://" + ip + ":" + port + "/restconf/config/opendaylight-inventory:nodes";
            string newURI;
            newURI = baseURIdeletes + "/node/" + node + "/table/" + table + "/flow/" + flow;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newURI);

            //Authorization para podermos fazer os requests

            string cre = String.Format("{0}:{1}", username, password);

            byte[] bytes = Encoding.ASCII.GetBytes(cre);

            string base64 = Convert.ToBase64String(bytes);

            request.Headers.Add("Authorization", "basic " + base64);

            //acaba aqui a parcela da authorization
            request.Method = "DELETE";



            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                MessageBox.Show(response.StatusCode.ToString());


            }

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader srReader = new StreamReader(stream))
                {
                    richTextBox1.Text = response.StatusCode.ToString();
                    richTextBox2.Clear();
                    MessageBox.Show("Flow deleted successfully!!");
                    return srReader.ReadToEnd();
                }

            }



        }

        #endregion

        #region Buttons_Opendaylight 

        private void buttonGetSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIpODL.Text) && string.IsNullOrWhiteSpace(textBoxPortODL.Text))
            {
                richTextBox2.Clear();
                MessageBox.Show("You need to select your IP!!");
            }
            else if (string.IsNullOrEmpty(comboBoxNode.Text))
            {
                richTextBox2.Clear();
                MessageBox.Show("You need to select a Node!!");

            }
            else if (string.IsNullOrEmpty(comboBoxTable.Text) && string.IsNullOrEmpty(comboBoxFlow.Text))
            {
                richTextBox2.Clear();
                GetSpecificNode(textBoxIpODL.Text, textBoxPortODL.Text, textBoxUserODL.Text, textBoxPassODL.Text, comboBoxNode.SelectedItem.ToString());

            }
            else if (string.IsNullOrEmpty(comboBoxFlow.Text))
            {
                richTextBox2.Clear();
                GetSpecificTable(textBoxIpODL.Text, textBoxPortODL.Text, textBoxUserODL.Text, textBoxPassODL.Text, comboBoxNode.SelectedItem.ToString(), comboBoxTable.SelectedItem.ToString());
            }
            else
            {
                richTextBox2.Clear();
                GetSpecificFlow(textBoxIpODL.Text, textBoxPortODL.Text, textBoxUserODL.Text, textBoxPassODL.Text, comboBoxNode.SelectedItem.ToString(), comboBoxTable.SelectedItem.ToString(), comboBoxFlow.SelectedItem.ToString());
            }
        }

        private void buttonGetAllNodes_Click(object sender, EventArgs e)
        {

            richTextBox2.Clear();
            GetNodes(textBoxIpODL.Text, textBoxPortODL.Text, textBoxUserODL.Text, textBoxPassODL.Text);

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            deleteSpecificFlow(textBoxIpODL.Text, textBoxPortODL.Text, textBoxUserODL.Text, textBoxPassODL.Text, comboBoxNode.SelectedItem.ToString(), comboBoxTable.SelectedItem.ToString(), comboBoxFlow.SelectedItem.ToString());


        }

        private void buttonCreateFlow_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            if (checkBoxDrop.Checked)
            {
                CreateFlowDrop(textBoxIpODL.Text, textBoxPortODL.Text, textBoxUserODL.Text, textBoxPassODL.Text, comboBoxNode.SelectedItem.ToString(), comboBoxTable.SelectedItem.ToString(), textBoxNewFlow.Text, textBoxPriority.Text);
            }
            else
            {
                CreateFlow(textBoxIpODL.Text, textBoxPortODL.Text, textBoxUserODL.Text, textBoxPassODL.Text, comboBoxNode.SelectedItem.ToString(), comboBoxTable.SelectedItem.ToString(), textBoxNewFlow.Text, textBoxPriority.Text);
            }


            MessageBox.Show("Flow " + textBoxNewFlow.Text + " created with success");
            textBoxNewFlow.Clear();
            textBoxPriority.Clear();

        }

        #endregion





        //secção OPENSTACK

        #region methods OpenStack

        private string GetTokenScoped(string ip, string port, string username, string password, string project)
        {

            var client = new RestClient("http://" + ip + ":" + port + "/identity/v3/auth/tokens");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "0806bf5f-e291-490f-88a5-a5ef7aaf5ddd");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n    \"auth\": {\n        \"identity\": {\n            \"methods\": [\n                \"password\"\n            ],\n            \"password\": {\n                \"user\": {\n                    \"name\": \"" + username + "\",\n                    \"domain\": {\n                        \"name\": \"Default\"\n                    },\n                    \"password\": \"" + password + "\"\n                }\n            }\n        },\n        \"scope\": {\n            \"project\": {\n                \"id\": \"" + project + "\"\n            }\n        }\n    }\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response.Headers.ToList().Find(x => x.Name == "X-Subject-Token").Value.ToString();

        }

        private string GetTokenUnscoped(string ip, string port, string username, string password)
        {
            var client = new RestClient("http://" + ip + ":" + port + "/identity/v3/auth/tokens");
            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "f2443756-ea4a-4709-b52d-d483f3e35b70");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n    \"auth\": {\n        \"identity\": {\n            \"methods\": [\n                \"password\"\n            ],\n            \"password\": {\n                \"user\": {\n                    \"name\": \"" + username + "\",\n                    \"domain\": {\n                        \"name\": \"Default\"\n                    },\n                    \"password\": \"" + password + "\"\n                }\n            }\n        }\n        \n    }\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response.Headers.ToList().Find(x => x.Name == "X-Subject-Token").Value.ToString();
        }

        private List<Project> GetProjects(string ip, string port, string username, string password)
        {
            comboBoxProject.Items.Clear();
            string token = GetTokenUnscoped(ip, port, username, password);
            var client = new RestClient("http://" + ip + ":" + port + "/identity/v3/auth/projects");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "fb88f0aa-53b7-4037-848f-cc5b474c0f7e");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic am9hbzpqb2Fv");
            request.AddHeader("X-Auth-Token", token);
            IRestResponse response = client.Execute(request);

            var sResponse = response.Content;

            var jResponse = Deserialize.FromJson(sResponse);
            List<Project> projetos = jResponse.Projects;

            foreach (Project projeto in projetos)
            {
                comboBoxProject.Items.Add(projeto.Name);
                richTextBox3.AppendText("Name: " + projeto.Name + "\nID: " + projeto.Id + Environment.NewLine + Environment.NewLine);

            }
            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();

            return projetos;

        }

        private List<Network> GetNetworks(string ip, string port, string username, string password, string project)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/os-networks");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "d2b02e21-dac8-40da-9bf6-9bfb1b179457");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);

            var sResponse = response.Content;
            var jResponse = DeserializeNetworks.FromJson(sResponse);
            List<Network> networks = jResponse.Networks;

            foreach (Network network in networks)
            {
                comboBoxNetworks.Items.Add(network.label);
                richTextBox3.AppendText("Label: " + network.label + "\nId: " + network.id + Environment.NewLine + Environment.NewLine);
            }

            return networks;
        }

        private List<Server> GetInstances(string ip, string port, string username, string password, string project)
        {
            comboBoxInstance.Items.Clear();
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/detail");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "d1297249-132e-4c2c-acc6-3d6a96b87c38");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            var sResponse = response.Content;

            var jResponse = Deserialize.FromJson(sResponse);
            List<Server> instances = jResponse.Servers;

            foreach (Server instance in instances)
            {
                comboBoxInstance.Items.Add(instance.Name);
                richTextBox3.AppendText("Name: " + instance.Name + "\nId: " + instance.Id + "\nStatus: " + instance.Status + Environment.NewLine + Environment.NewLine);
            }
            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();

            return instances;
        }

        private List<Image> GetImages(string ip, string port, string username, string password, string project)
        {
            comboBoxImage.Items.Clear();
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/images");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "52af9a43-9dd9-45b5-800d-dee69d847477");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            var sResponse = response.Content;
            var jResponse = Deserialize.FromJson(sResponse);
            List<Image> images = jResponse.Images;

            foreach (Image image in images)
            {
                comboBoxImage.Items.Add(image.Name);
                richTextBox3.AppendText("Name: " + image.Name + "\nID: " + image.Id + Environment.NewLine + Environment.NewLine);
            }

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            return images;
        }

        private List<Flavor> GetFlavors(string ip, string port, string username, string password, string project)
        {
            comboBoxFlavor.Items.Clear();
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/flavors/detail");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "5899feec-0394-431f-acc4-4652276d0500");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            var sResponse = response.Content;
            var jResponse = DeserializeFlavors.FromJson(sResponse);
            List<Flavor> flavors = jResponse.Flavors;

            foreach (Flavor flavor in flavors)
            {
                comboBoxFlavor.Items.Add(flavor.Name);
                richTextBox3.AppendText("Name: " + flavor.Name + "\nLink: " + flavor.Links[0].Href + "\nId: " + flavor.Id + Environment.NewLine + Environment.NewLine);
            }
            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            return flavors;
        }

        private void GetUsageReport(string ip, string port, string username, string password, string project)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/os-simple-tenant-usage/" + projeto);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "3a92ab32-edca-4a32-ad71-4f948a4b5218");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            var sResponse = response.Content;
            var jResponse = DeserializeUsageReport.FromJson(sResponse);

            List<ServerUsage> serverUsages = jResponse.TenantUsage.ServerUsages;
            richTextBox3.AppendText("Server Usage:\n\n");
            foreach (ServerUsage serverUsage in serverUsages)
            {
                richTextBox3.AppendText("Instance ID: " + serverUsage.InstanceId + "\nInstance Name: " + serverUsage.Name + "\nUptime: " + serverUsage.Uptime + "\nRAM: " + serverUsage.MemoryMb + "MB\nDisk: " + serverUsage.LocalGb + "GB\nVCPUs: " + serverUsage.Vcpus + "\nFlavor:" + serverUsage.Flavor + "\nState: " + serverUsage.State + Environment.NewLine + Environment.NewLine);
            }
        }

        private void CreateInstance(string ip, string port, string username, string password, string project, string instname, string imgref, string flvref, string networkLabel)
        {

            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxImage.Items.Clear();
            var imagem = GetImages(ip, port, username, password, project).ToList().Find(x => x.Name == imgref).Id;
            comboBoxImage.Refresh();
            comboBoxFlavor.Items.Clear();
            var flavor = GetFlavors(ip, port, username, password, project).ToList().Find(x => x.Name == flvref).Links[0].Href;
            comboBoxFlavor.Refresh();
            comboBoxNetworks.Items.Clear();
            var network = GetNetworks(ip, port, username, password, project).ToList().Find(x => x.label == networkLabel).id;
            comboBoxNetworks.Refresh();

            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "17f89025-3a82-424d-af68-4c162c1e8751");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n    \"server\": {\r\n        \"name\": \"" + instname + "\",\r\n        \"imageRef\": \"" + imagem + "\",\r\n        \"flavorRef\": \"" + flavor + "\",\r\n        \"OS-DCF:diskConfig\": \"AUTO\",\r\n        \"security_groups\": [\r\n            {\r\n                \"name\": \"default\"\r\n            }\r\n        ],\r\n        \"networks\": [\r\n        \t{\r\n        \t\t\"uuid\": \"" + network + "\"\r\n        \t}\r\n        ]\r\n    }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var sResponse = response.Content;

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance " + instname + " created successfully!!");
            richTextBox3.Text = JsonConvert.DeserializeObject<dynamic>(sResponse).ToString();
        }

        private void DeleteInstance(string ip, string port, string username, string password, string project, string instance)
        {

            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();

            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "42ae929c-8aee-4349-a8ef-eafc4ec75019");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            comboBoxInstance.Refresh();
            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance deleted successfully!!");
        }

        private void PauseInstance(string ip, string port, string username, string password, string project, string instance)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "/action");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "7ae64b5c-487c-428e-8d3c-b03384ce9f93");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"pause\": null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance paused successfully!!");
        }

        private void UnpauseInstance(string ip, string port, string username, string password, string project, string instance)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "/action");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "a5cd0063-12d0-4ceb-bc84-6d906afc7d68");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"unpause\": null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance unpaused successfully!!");
        }

        private void SuspendInstance(string ip, string port, string username, string password, string project, string instance)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "/action");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "3736ec44-dd8a-41ff-9756-8352c675ab5a");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"suspend\": null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance suspended successfully!!");
        }

        private void ResumeInstance(string ip, string port, string username, string password, string project, string instance)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "/action");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "7d5f0c00-0048-49a9-b85a-f503801e52c4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"resume\": null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance resumed successfully!!");
        }

        private void StopInstance(string ip, string port, string username, string password, string project, string instance)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "/action");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "1e9240ff-ede1-4061-8515-ef3a5948c16b");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"os-stop\": null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance stopped successfully!!");
        }

        private void StartInstance(string ip, string port, string username, string password, string project, string instance)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "/action");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "a4fa6499-7648-4ddf-b3c5-2164267f0b2c");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"os-start\": null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance started successfully!!");
        }

        private void RebootInstance(string ip, string port, string username, string password, string project, string instance)
        {
            comboBoxProject.Items.Clear();
            var projeto = GetProjects(ip, port, username, password).ToList().Find(x => x.Name == project).Id;
            comboBoxProject.Refresh();
            comboBoxInstance.Items.Clear();
            var inst = GetInstances(ip, port, username, password, project).ToList().Find(x => x.Name == instance).Id;
            comboBoxInstance.Refresh();
            richTextBox3.Clear();
            string token = GetTokenScoped(ip, port, username, password, projeto);
            var client = new RestClient("http://" + ip + ":" + port + "/compute/v2.1/servers/" + inst + "/action");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password)));
            request.AddHeader("Postman-Token", "5a1d47ce-efdc-423f-b460-392ab5486ed9");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("X-Auth-Token", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"reboot\": {\n\t\t\"type\" : \"HARD\"\n\t}\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            richTextBox4.Clear();
            richTextBox4.Text = response.StatusCode.ToString();
            MessageBox.Show("Instance rebooted successfully!!");
        }
        #endregion

        #region Buttons_OpenStack

        private void buttonGetInstances_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) && string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) && string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else
            {

                richTextBox3.Clear();
                GetInstances(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString());
                comboBoxInstance.Refresh();
                comboBoxProject.Refresh();

            }

        }

        private void buttonCreateInst_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxInstName.Text) || string.IsNullOrEmpty(comboBoxImage.SelectedItem.ToString()) || string.IsNullOrEmpty(comboBoxFlavor.SelectedItem.ToString()))
            {
                richTextBox3.Clear();
                MessageBox.Show("There are creation fields empty, please insert the data necessary for the creation of the Instance!!");
            }
            else
            {

                CreateInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString(), textBoxInstName.Text, comboBoxImage.SelectedItem.ToString(), comboBoxFlavor.SelectedItem.ToString(), comboBoxNetworks.SelectedItem.ToString());
                textBoxInstName.Clear();
                comboBoxFlavor.Refresh();
                comboBoxImage.Refresh();
                comboBoxProject.Refresh();

            }

        }

        private void buttonDeleteInst_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else if (string.IsNullOrEmpty(comboBoxInstance.SelectedItem.ToString()))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Instance selected, please insert the Instance you need!!");
            }
            else
            {

                DeleteInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString(), comboBoxInstance.SelectedItem.ToString());
                richTextBox3.Clear();
                comboBoxInstance.Refresh();


                comboBoxProject.Refresh();


            }
        }

        private void buttonGetProjects_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else
            {

                richTextBox3.Clear();
                GetProjects(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text);
            }

        }

        private void buttonGetImages_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you want!!");
            }
            else
            {

                richTextBox3.Clear();
                GetImages(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString());
                comboBoxImage.Refresh();
                comboBoxProject.Refresh();


            }
        }

        private void buttonGetFlavors_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you want!!");
            }
            else
            {

                richTextBox3.Clear();
                GetFlavors(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString());
                comboBoxFlavor.Refresh();
                comboBoxProject.Refresh();


            }
        }

        private void buttonUsage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you want!!");
            }
            else
            {
                richTextBox3.Clear();
                GetUsageReport(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString());
            }
        }

        private void buttonPauseUnpause_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else if (string.IsNullOrEmpty(comboBoxInstance.SelectedItem.ToString()))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Instance selected, please insert the Instance you need!!");
            }
            else
            {
                var instname = comboBoxInstance.SelectedItem.ToString();
                var projectName = comboBoxProject.SelectedItem.ToString();
                richTextBox3.Clear();
                var instState = GetInstances(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, projectName).ToList().Find(x => x.Name == instname).Status;
                richTextBox3.Clear();
                if (instState.ToString() == "PAUSED")
                {
                    UnpauseInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, projectName, instname);
                }

                if (instState.ToString() == "ACTIVE")
                {
                    PauseInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, projectName, instname);
                }
            }
        }

        private void buttonSuspendResume_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else if (string.IsNullOrEmpty(comboBoxInstance.SelectedItem.ToString()))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Instance selected, please insert the Instance you need!!");
            }
            else
            {
                var instname = comboBoxInstance.SelectedItem.ToString();
                var projectName = comboBoxProject.SelectedItem.ToString();
                richTextBox3.Clear();
                var instState = GetInstances(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString()).ToList().Find(x => x.Name == instname).Status;
                richTextBox3.Clear();
                if (instState.ToString() == "SUSPENDED")
                {
                    ResumeInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, projectName, instname);
                }

                if (instState.ToString() == "ACTIVE")
                {
                    SuspendInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, projectName, instname);
                }
            }
        }

        private void buttonStopStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else if (string.IsNullOrEmpty(comboBoxInstance.SelectedItem.ToString()))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Instance selected, please insert the Instance you need!!");
            }
            else
            {
                var instname = comboBoxInstance.SelectedItem.ToString();
                var projectName = comboBoxProject.SelectedItem.ToString();
                richTextBox3.Clear();
                var instState = GetInstances(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString()).ToList().Find(x => x.Name == instname).Status;
                richTextBox3.Clear();
                if (instState.ToString() == "SHUTOFF")
                {
                    StartInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, projectName, instname);
                }

                if (instState.ToString() == "ACTIVE")
                {
                    StopInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, projectName, instname);
                }
            }
        }

        private void buttonReboot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) || string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) || string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else if (string.IsNullOrEmpty(comboBoxInstance.SelectedItem.ToString()))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Instance selected, please insert the Instance you need!!");
            }
            else
            {
                richTextBox3.Clear();

                RebootInstance(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString(), comboBoxInstance.SelectedItem.ToString());
            }
        }

        private void buttonNetworks_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserOPS.Text) && string.IsNullOrWhiteSpace(textBoxPassOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No username/password, please insert username/password!!");
            }
            else if (string.IsNullOrWhiteSpace(textBoxIpOPS.Text) && string.IsNullOrWhiteSpace(textBoxPortOPS.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No IP/Port, you need to select an IP and a Port!!");
            }
            else if (string.IsNullOrEmpty(comboBoxProject.Text))
            {
                richTextBox3.Clear();
                MessageBox.Show("No Project selected, please insert the project you need!!");
            }
            else
            {
                richTextBox3.Clear();
                GetNetworks(textBoxIpOPS.Text, textBoxPortOPS.Text, textBoxUserOPS.Text, textBoxPassOPS.Text, comboBoxProject.SelectedItem.ToString());
                comboBoxNetworks.Refresh();
                comboBoxProject.Refresh();


            }
        }

        #endregion


    }

    #region enums

    public enum Rel { Bookmark, Self };

    public enum FlowNodeInventoryAdvertisedFeatures { Empty };

    public enum FlowNodeInventoryCurrentFeature { Empty, TenGbFdCopper };

    #endregion
}
