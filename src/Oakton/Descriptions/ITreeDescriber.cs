﻿using Spectre.Console;

namespace Oakton.Descriptions
{
    /// <summary>
    /// Interface to expose additional diagnostic information to a Spectre tree node
    /// </summary>
    public interface ITreeDescriber
    {
        void Describe(TreeNode parentNode);
    }
}