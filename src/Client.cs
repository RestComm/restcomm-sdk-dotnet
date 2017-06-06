﻿// /*
//  * TeleStax, Open Source Cloud Communications
//  * Copyright 2011-2016, Telestax Inc and individual contributors
//  * by the @authors tag.
//  *
//  * This is free software; you can redistribute it and/or modify it
//  * under the terms of the GNU Lesser General Public License as
//  * published by the Free Software Foundation; either version 2.1 of
//  * the License, or (at your option) any later version.
//  *
//  * This software is distributed in the hope that it will be useful,
//  * but WITHOUT ANY WARRANTY; without even the implied warranty of
//  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  * Lesser General Public License for more details.
//  *
//  * You should have received a copy of the GNU Lesser General Public
//  * License along with this software; if not, write to the Free
//  * Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
//  * 02110-1301 USA, or see the FSF site: http://www.fsf.org.
//  */
//
using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace RestComm
{
	
	public partial class Account{
		public List<Client>  GetClientList(){
			RestClient client = new RestClient (baseurl+"Accounts/"+Properties.Sid+"/Clients");
			RestRequest request = new RestRequest (Method.GET);
			client.Authenticator=new HttpBasicAuthenticator (Properties.Sid, Properties.authtoken);
			IRestResponse response = client.Execute (request);
			List<Client> clientslist = new List<Client> ();

			List<string> sidlist = response.Content.GetAccountsProperty ("Sid");
			int i = sidlist.Count;
			for (int j = 0; j < i; j++) {
				clientslist.Add (new Client (response.Content, j));

			}
			return clientslist;

		}
		public MakeClient makeclient(string Login,string Password){
			RestClient client = new RestClient (baseurl+"Accounts/"+Properties.Sid+"/Clients");
			RestRequest request = new RestRequest (Method.POST);
			request.AddParameter ("Login",Login);
			request.AddParameter ("Password", Password);
			client.Authenticator=new HttpBasicAuthenticator (Properties.Sid, Properties.authtoken);

			return new MakeClient (client, request);

		}

	}
	public class Client
	{
		public string Sid;
		public string DateCreated;
		public string DateUpdated;
		public string FriendlyName;
		public string AccountSid;
		public string ApiVersion;
		public string Login;
		public string Password;
		public string Status;
		public string VoiceUrl;
		public string VoiceMethod;
		public string VoiceFallbackUrl;
		public string VoiceFallbackMethod;
		public string VoiceApplication;
		public string Uri;
		
		public Client (string responsecontent,int  clientno)
		{
			Sid = responsecontent.GetAccountsProperty ("Sid")[clientno];
			DateCreated = responsecontent.GetAccountsProperty ("DateCreated")[clientno];
			DateUpdated = responsecontent.GetAccountsProperty ("DateUpdated")[clientno];
			FriendlyName = responsecontent.GetAccountsProperty ("FriendlyName")[clientno];
			AccountSid = responsecontent.GetAccountsProperty ("AccountSid")[clientno];
			ApiVersion = responsecontent.GetAccountsProperty ("ApiVersion")[clientno];
			Login = responsecontent.GetAccountsProperty ("Login")[clientno];
			Password = responsecontent.GetAccountsProperty ("Password")[clientno];
			Status = responsecontent.GetAccountsProperty ("Status")[clientno];
			VoiceUrl = responsecontent.GetAccountsProperty ("VoiceUrl")[clientno];
			VoiceMethod = responsecontent.GetAccountsProperty ("VoiceMethod")[clientno];
			VoiceFallbackUrl = responsecontent.GetAccountsProperty ("VoiceFallbackUrl")[clientno];
			VoiceFallbackMethod = responsecontent.GetAccountsProperty ("VoiceFallbackMethod")[clientno];
			VoiceApplication = responsecontent.GetAccountsProperty ("VoiceApplication")[clientno];
			Uri = responsecontent.GetAccountsProperty ("Uri")[clientno];
		}
		public Client (string responsecontent)
		{
			Sid = responsecontent.GetAccountProperty ("Sid");
			DateCreated = responsecontent.GetAccountProperty ("DateCreated");
			DateUpdated = responsecontent.GetAccountProperty ("DateUpdated");
			FriendlyName = responsecontent.GetAccountProperty ("FriendlyName");
			AccountSid = responsecontent.GetAccountProperty ("AccountSid");
			ApiVersion = responsecontent.GetAccountProperty ("ApiVersion");
			Login = responsecontent.GetAccountProperty ("Login");
			Password = responsecontent.GetAccountProperty ("Password");
			Status = responsecontent.GetAccountProperty ("Status");
			VoiceMethod = responsecontent.GetAccountProperty ("VoiceMethod");
			VoiceFallbackMethod = responsecontent.GetAccountProperty ("VoiceFallbackMethod");
			Uri = responsecontent.GetAccountProperty ("Uri");
		}
		public void Delete(String sid, string authtoken){
			RestClient client = new RestClient (Account.baseurl+"Accounts/"+sid+"/Clients/"+Sid);
			RestRequest request = new RestRequest (Method.DELETE);
			client.Authenticator = new HttpBasicAuthenticator (sid, authtoken);
			client.Execute (request);

		}
		public void ChangePassword(String sid, string authtoken,string NewPassword){
			RestClient client = new RestClient (Account.baseurl+"Accounts/"+sid+"/Clients/"+Sid);
			RestRequest request = new RestRequest (Method.PUT);
			client.Authenticator = new HttpBasicAuthenticator (sid, authtoken);
			request.AddParameter ("Password", NewPassword);
			client.Execute (request);


		}
	}
	public class MakeClient{
		RestClient client;
		RestRequest request;
		public MakeClient(RestClient client,RestRequest request){
			this.client = client;
			this.request = request;
		}
		public void AddParameter(string ParameterName,string ParameterValue){
			request.AddParameter (ParameterName, ParameterValue);
		}
		public Client Create(){

			IRestResponse response = client.Execute (request);
			return new Client (response.Content);

		}

	}



}

