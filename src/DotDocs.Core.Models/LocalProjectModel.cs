﻿using DotDocs.Core.Models.Language;
using System.Text.Json.Serialization;

namespace DotDocs.Core.Models
{
    /// <summary>
    /// Represents a local project with a .csproj file.
    /// </summary>
    public class LocalProjectModel
    {
        /// <summary>
        /// Contains all the projects declared in this project.
        /// </summary>
        [JsonIgnore]
        public List<TypeModel> _DefinedTypes { get; } = new();
        /// <summary>
        /// Just the project name with no extension.
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Directory where the .csproj file resides.
        /// </summary>
        public string ProjectDirectory { get; set; }
        /// <summary>
        /// Just the file name.
        /// </summary>
        public string ProjectFileName { get; set; }
        /// <summary>
        /// Entire path to file include name with extension.
        /// </summary>
        public string ProjectPath { get; set; }
        /// <summary>
        /// Contains unique identifiers to local projects that are dependencies.
        /// </summary>
        public string[] LocalProjects 
            => _LocalProjects
            .Select(project => project.GetProjectId())
            .ToArray();
        /// <summary>
        /// A unique identifier to the assembly this project produces.
        /// </summary>
        public string AssemblyId { get; set; }
        /// <summary>
        /// A unique idenfitier for this project.
        /// </summary>
        public string Id => this.GetProjectId();
        /// <summary>
        /// Collection of all <see cref="LocalProjectModel"/> dependencies.
        /// </summary>   
        [JsonIgnore]
        public List<LocalProjectModel> _LocalProjects { get; set; } = new();
        /// <summary>
        /// The assembly model instance this project generates.
        /// </summary>
        [JsonIgnore]
        public AssemblyModel _Assembly { get; set; }
        /// <summary>
        /// Determines if a projectFile exists recursively from here down into other projects.
        /// Uses a depth-first-search (DFS) approach.
        /// </summary>
        /// <param name="projectFile">The project file in question.</param>
        /// <returns>A reference to the <see cref="LocalProjectModel"/> instance or null.</returns>
        public bool Exists(string projectFile)
        {
            foreach (var proj in _LocalProjects)
            {
                if (proj.ProjectPath == projectFile) // base case
                    return true;
                return proj.Exists(projectFile);
            }
            return false;
        }
    }
}
