﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData;

namespace Microsoft.Test.AspNet.OData
{
    internal static class FormatterTestHelper
    {
        internal static ODataMediaTypeFormatter GetFormatter(ODataPayloadKind[] payload, HttpRequestMessage request)
        {
            ODataMediaTypeFormatter formatter;
            formatter = new ODataMediaTypeFormatter(payload);
            formatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataMinimalMetadata));
            formatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationXml));

            request.GetConfiguration().Routes.MapFakeODataRoute();
            formatter.Request = request;
            return formatter;
        }

        internal static ObjectContent<T> GetContent<T>(T content, ODataMediaTypeFormatter formatter, string mediaType)
        {
            return new ObjectContent<T>(content, formatter, MediaTypeHeaderValue.Parse(mediaType));
        }

        internal static Task<string> GetContentResult(ObjectContent content, HttpRequestMessage request)
        {
            // request is not needed on AspNet.
            return content.ReadAsStringAsync();
        }

        internal static HttpContentHeaders GetContentHeaders(string contentType = null)
        {
            var headers = new StringContent(String.Empty).Headers;
            if (!string.IsNullOrEmpty(contentType))
            {
                headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            }

            return headers;
        }
    }
}