﻿using AndreasReitberger.API.OctoPrint.Models;
using AndreasReitberger.API.Print3dServer.Core;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.JSON.System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.OctoPrint
{
    public partial class OctoPrintClient
    {

#if DEBUG
        #region Debug

        [ObservableProperty]
        JsonSerializerOptions jsonSerializerSettings = DefaultJsonSerializerSettings;

        public new static JsonSerializerOptions DefaultJsonSerializerSettings = new()
        {
            // Detect if the json respone has more or less properties than the target class
            //MissingMemberHandling = MissingMemberHandling.Error,
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true,
            Converters =
            {                     
                // Map the converters
                new TypeMappingConverter<IAuthenticationHeader, AuthenticationHeader>(),
                new TypeMappingConverter<IPrinter3d, OctoPrintPrinter>(),
            }
        };
        #endregion
#else
        #region Release
        public static JsonSerializerOptions DefaultJsonSerializerSettings = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true,
            Converters =
            {                     
                // Map the converters
                new TypeMappingConverter<IAuthenticationHeader, AuthenticationHeader>(),
                new TypeMappingConverter<IGcodeMeta, KlipperGcodeMetaResult>(),
                new TypeMappingConverter<IGcodeImage, KlipperGcodeThumbnail>(),
                new TypeMappingConverter<IPrint3dJob, KlipperJobQueueItem>(),
            }
        };
        #endregion
#endif
    }
}
