﻿using System;
using StormshrikeTODO.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace StormshrikeTODO.Model
{
    public class Session
    {
        private IPersistence _savePersistence;
        private IPersistence _loadPersistence;
        private IPersistence _persistence;

        private Collection<Project> _projectList;

        public DefinedContexts Contexts { get; set; }
        public bool Initialized { get { return _projectList != null; } }

        public int ProjectCount { get { return _projectList == null ? 0 : _projectList.Count; } }

        public Session(IPersistence persistence)
        {
            _persistence = persistence;
            _savePersistence = persistence;
            _loadPersistence = persistence;
        }

        public Session(IPersistence savePersistence, IPersistence loadPersistence)
        {
            _savePersistence = savePersistence;
            _loadPersistence = loadPersistence;
        }

        public void LoadContexts()
        {
            this.Contexts = _loadPersistence.LoadContexts();
        }

        public void LoadProjects()
        {
            _projectList = _loadPersistence.LoadProjects();
        }

        public void Save()
        {
            _savePersistence.SaveProjects(_projectList);
            _savePersistence.SaveContexts(this.Contexts);
        }

        public IEnumerable<Project> ListProjectsWithNoTasks()
        {
            return _projectList != null ? _projectList.Where(p => !p.HasTasks) : null;
        }

        public Project FindProjectByID(string prjId)
        {
            return _projectList?.Where(value => value.UniqueID.ToString() == prjId).FirstOrDefault();
        }

        public void AddProject(Project prj)
        {
            _projectList.Add(prj);
        }

        public System.Collections.Generic.IEnumerable<Project> ProjectEnumerable()
        {
            if (_projectList == null)
            {
                yield break;
            }

            foreach (var prj in _projectList)
            {
                yield return prj;
            }
        }

        public void RemoveProject(String prjID)
        {
            var prj = FindProjectByID(prjID);
            _projectList.Remove(prj);
        }

        public void LoadDefaultContexts()
        {
            this.Contexts = new DefaultContextGenerator().GenerateDefaultContexts();
        }
    }
}