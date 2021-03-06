﻿using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface ICustomErrorProvider : ISiteElementProvider<CustomError>
    {
        void Export(Site site, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);

    }
}