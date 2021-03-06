﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Google.Type;
using Java.Util;
using ProgettoEsame.Model;
using ProgettoEsame.ViewModel.Helpers;
using Xamarin.Forms;
using Firebase.Firestore;
using Android.Gms.Tasks;
using Android.Service.VR;
using Org.W3c.Dom;
[assembly: Dependency(typeof(ProgettoEsame.Droid.Dependencies.FirestoreLunedi))]
namespace ProgettoEsame.Droid.Dependencies
{
    class FirestoreLunedi : Java.Lang.Object, ViewModel.Helpers.LunediFirestore, IOnCompleteListener
    {

        List<Evento> lunediList;
        bool hasReadLunedi = false;

        public FirestoreLunedi()
        {
            lunediList = new List<Evento>();
        }



        public async Task<bool> DeleteLunedi(Evento lunedi)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("users").Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid).Collection("Lunedi");
                collection.Document(lunedi.Id).Delete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InsertLunedi(Evento lunedi)
        {

            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("users").Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid).Collection("Lunedi");
                var lunediDocument = new Dictionary<string, Java.Lang.Object>
                {
                    {"nome", lunedi.Name },
                    {"luogo", lunedi.Luogo },
                    {"ora inizio", lunedi.OraI },
                    {"ora fine", lunedi.OraF }

                };
                collection.Add(lunediDocument);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<IList<Evento>> ReadLunedi()
        {
            hasReadLunedi = false;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("users").Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid).Collection("Lunedi");
            collection.Get().AddOnCompleteListener(this);
            for (int i = 0; i < 25; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (hasReadLunedi)
                    break;
            }

            return lunediList;

        }

        public async Task<bool> UpdateLunedi(Evento lunedi)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("users").Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid).Collection("Lunedi");
                collection.Document(lunedi.Id).Update("nome", lunedi.Name, "luogo", lunedi.Luogo, "ora inizio", lunedi.OraI, "ora fine", lunedi.OraF);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;
                lunediList.Clear();
                foreach (var doc in documents.Documents)
                {
                    string luogo, oraI, oraF;
                    if (doc.Get("luogo") == null)
                    {
                        luogo = "";
                    }
                    else
                    {
                        luogo = doc.Get("luogo").ToString();
                    }
                    if (doc.Get("ora inizio") == null)
                    {
                        oraI = "";
                    }
                    else
                    {
                        oraI= doc.Get("ora inizio").ToString();
                    }
                    if (doc.Get("ora fine") == null)
                    {
                        oraF = "";
                    }
                    else
                    {
                        oraF = doc.Get("ora fine").ToString();
                    }
                    Evento lunedi = new Evento
                    {
                        Name = doc.Get("nome").ToString(),
                        Luogo = luogo,
                        OraI = oraI,
                        OraF = oraF,

                        Id = doc.Id
                    };

                    lunediList.Add(lunedi);

                }
            }
            else
            {
                lunediList.Clear();

            }
            hasReadLunedi = true;
        }
    }
}