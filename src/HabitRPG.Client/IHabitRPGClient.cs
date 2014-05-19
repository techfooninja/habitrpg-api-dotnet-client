﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace HabitRPG.Client
{
  public interface IHabitRPGClient
  {
    Task<T> CreateTask<T>(T task) where T : Model.Task;

    Task<List<Model.Task>> GetTasks();
  }
}