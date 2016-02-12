﻿using Kentor.AuthServices.Saml2P;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;

namespace Kentor.AuthServices.StubIdp.Models
{
    public class LogoutModel
    {
        [DisplayName("Incoming LogoutRequest")]
        public string LogoutRequestXml { get; set; }

        [DisplayName("InResponseTo")]
        public string InResponseTo { get; set; }

        [Required]
        [DisplayName("SP Single Logout Url")]
        public Uri DestinationUrl { get; set; }

        [Required]
        [DisplayName("Session index")]
        public string SessionIndex { get; set; }

        [Required]
        [DisplayName("Subject NameID")]
        public string NameId { get; set; }

        public Saml2LogoutResponse ToLogoutResponse()
        {
            return new Saml2LogoutResponse(Saml2StatusCode.Success)
            {
                DestinationUrl = DestinationUrl,
                SigningCertificate = CertificateHelper.SigningCertificate,
                InResponseTo = new Saml2Id(InResponseTo)
            };
        }

        public Saml2LogoutRequest ToLogoutRequest()
        {
            return new Saml2LogoutRequest()
            {
                DestinationUrl = DestinationUrl,
                SigningCertificate = CertificateHelper.SigningCertificate,
                Issuer = new EntityId(UrlResolver.MetadataUrl.ToString()),
                NameId = new Saml2NameIdentifier(NameId),
                SessionIndex = SessionIndex
            };
        }
    }
}