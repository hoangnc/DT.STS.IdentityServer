﻿using DT.STS.IdentityServer.Common.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text.RegularExpressions;
using Wdc.DirectoryLib.Types;
namespace DT.STS.IdentityServer.Common.Helpers
{
    /// <summary>
    /// Using this class you can inquiry the user's detail from AD, Check a user current status from AD and also Change the password of users.
    /// </summary>
    public static class ActiveDirectoryHelper
    {
        /// <summary>
        /// Returns UserAccount object from a given search result
        /// </summary>
        /// <param name="result">SearchResult computed by one of the other GetUser methods</param>
        public static AdUserAccount GetUserFromResult(SearchResult result)
        {
            var userAccountControl = TryGetResult<int>(result, "userAccountControl");
            var active = userAccountControl == 512 ? true : false;

            // Values can be found here:
            // http://msdn.microsoft.com/en-us/library/ms679021(v=vs.85).aspx
            return new AdUserAccount()
            {
                ObjectGuid = TryGetResult<byte[]>(result, "objectGUID"),
                CommonName = TryGetResult<string>(result, "cn"),
                DisplayName = TryGetResult<string>(result, "displayName"),
                GivenName = TryGetResult<string>(result, "givenName"),
                SurName = TryGetResult<string>(result, "sn"),
                Email = TryGetResult<string>(result, "mail"),
                Department = TryGetResult<string>(result, "department"),
                Title = TryGetResult<string>(result, "title"),
                LocalityName = TryGetResult<string>(result, "l"),
                StateOrProvinceName = TryGetResult<string>(result, "st"),
                CountryName = TryGetResult<string>(result, "c"),
                Phone = TryGetResult<string>(result, "telephoneNumber"),
                Mobile = TryGetResult<string>(result, "mobile"),
                PhysicalDeliveryOfficeName = TryGetResult<string>(result, "physicalDeliveryOfficeName"),
                Description = TryGetResult<string>(result, "description"),
                JpegPhoto = TryGetResult<byte[]>(result, "thumbnailPhoto"),
                DistinguishedName = TryGetResult<string>(result, "distinguishedName"),
                SamAccountName = TryGetResult<string>(result, "samAccountName"),
                UserPrincipalName = TryGetResult<string>(result, "userPrincipalName"),
                Manager = TryGetResult<string>(result, "manager"),
                Active = active,
                DirectReports = TryGetResultList<string>(result, "directReports"),
            };
        }

        public static PrincipalContext GetPrincipalContext()
        {
            return new PrincipalContext(ContextType.Domain);
        }

        public static UserPrincipal GetUser(IdentityType identityType, string identityValue)
        {
            var res = UserPrincipal.FindByIdentity(GetPrincipalContext(), identityType, identityValue);
            return res;
        }

        public static IEnumerable<string> GetGroupsForUser(UserPrincipal userPrincipal)
        {
            var groups = new List<string>();
            var principalGroups = userPrincipal.GetGroups();
            foreach (var principalGroup in principalGroups)
            {
                groups.Add(principalGroup.Name);
            }
            return groups.Distinct().ToList();
        }

        public static T TryGetResult<T>(SearchResult result, string key)
        {
            ResultPropertyValueCollection valueCollection = result.Properties[key];
            if (valueCollection.Count > 0)
                return (T)valueCollection[0];
            else
                return default(T);
        }

        public static List<T> TryGetResultList<T>(SearchResult result, string key)
        {
            List<T> list = new List<T>();
            ResultPropertyValueCollection valueCollection = result.Properties[key];
            if (valueCollection.Count > 0)
            {
                foreach (T val in valueCollection)
                {
                    list.Add(val);
                }
            }
            return list;
        }
    }
}
