using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TaskTracker.Core.Models;
using TaskStatus = TaskTracker.Core.Models.TaskStatus;
namespace TaskTracker.Core.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;
    public TaskItem Add(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название не может быть пустым.");
            var task = new TaskItem
            {
                Id = _nextId++,
                Title = title.Trim(),
                Status = TaskStatus.New
            };
        _tasks.Add(task);
    
            return task;
    }

    public object Add(object title)
    {
        throw new NotImplementedException();
    }

    public List<TaskItem> GetAll()
    {
        // Возвращаем копию, чтобы внешний код не ломал список
    return _tasks.ToList();
    }

    private TaskItem GetExisting(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
            throw new ArgumentException($"Задача с Id={id} не найдена.");
        return task;
    }
    public TaskItem ChangeStatus(int id, TaskStatus newStatus)
    {
        var task = GetExisting(id);
        task.Status = newStatus;
        return task;
    }
    public void Delete(int id)
    {
        var task = GetExisting(id);
        _tasks.Remove(task);
    }
}