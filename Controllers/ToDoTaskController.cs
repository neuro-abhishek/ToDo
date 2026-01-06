using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json.Schema;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Produces("application/json")]
[ApiController]
[Route("/api/task")]
public class ToDoTaskController : ControllerBase
{
    ITaskOperationInterface _taskOperation;
    public ToDoTaskController(ITaskOperationInterface toDoTaskOperationObject)
    {
        _taskOperation = toDoTaskOperationObject;
    }

    /// <summary>
    /// Get All Tasks
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<ToDoTask>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetTasks()
    {
        try
        {
            List<ToDoTask> result = await _taskOperation.GetData();
            Response<List<ToDoTask>> res = new Response<List<ToDoTask>>(result);
            res.StatusCode = 200;
            res.Message = "Data is fetched successfully";
            return Ok(res);
        }   
        catch (System.Exception e)
        {
            Console.WriteLine("Error while fetching all tasks");
            Response<string> res = new Response<string>("");
            res.StatusCode = 500;
            res.ErrorMessage = e.Message;
            return Ok(res);
        }
    }

    /// <summary>
    /// Create a new task
    /// </summary>
    /// <param name="data">Task Data</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] ToDoTask data)
    {
        try
        {
            Response<string> res = new Response<string>("");
            bool returnValue = await _taskOperation.AddData(data);
            if (!returnValue)
            {
                res.StatusCode = 409;
                res.ErrorMessage = "Task with this id already exist";
                return Conflict(res);
            }
            else
            {
                res.StatusCode = 200;
                res.Message = "Task Created SuccessFully";
                return Ok(res);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("Error in Creating new Task " + e.Message);
            Response<string> res = new Response<string>("");
            res.StatusCode = 500;
            res.ErrorMessage = e.Message;
            return Ok(res);
        }
    }

    /// <summary>
    /// Get a task with a particular id
    /// </summary>
    /// <param name="id">Task Id</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ToDoTask>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTaskById([FromRoute] string id)
    {
        try
        {
            ToDoTask result = await _taskOperation.GetDataById(id);
            if (result.Name == null)
            {
                Response<string> res = new Response<string>("");
                res.StatusCode = 404;
                res.Message = "There is no task with id " + id;
                res.ErrorMessage = "There is no task with id " + id;

                return NotFound(res);
            }
            else
            {
                Response<ToDoTask> res = new Response<ToDoTask>(result);
                res.StatusCode = 200;
                res.Message = "Data Fetched Successfully";
                return Ok(res);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("Error while fetching task by id");
            Response<string> res = new Response<string>("");
            res.StatusCode = 500;
            res.ErrorMessage = e.Message;
            return Ok(res);
        }
    }

    /// <summary>
    /// Delete a task by a particular id
    /// </summary>
    /// <param name="id">Task Id</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute] string id)
    {
        try
        {
            bool resultValue = await _taskOperation.DeleteDataById(id);
            Response<string> res = new Response<string>("");
            if (resultValue)
            {
                res.StatusCode = 200;
                res.Message = "Task Deleted Successfully";
                return Ok(res);
            }
            else
            {
                res.StatusCode = 404;
                res.ErrorMessage = "There is no Task with this particular id";
                return NotFound(res);
            }
        }
        catch (System.Exception e)
        {
            Response<string> res = new Response<string>("");
            res.StatusCode = 500;
            res.ErrorMessage = e.Message;
            return Ok(res);
        }
    }

    /// <summary>
    /// Change Description of a task with a particular id
    /// </summary>
    /// <param name="id">Task Id</param>
    /// <param name="data">Updated Data</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
    [HttpPatch("{id}")]
    public async Task<IActionResult> TaskDescriptionUpdate([FromRoute] string id, [FromBody] ToDoTask data)
    {
        try
        {
            bool resultValue = await _taskOperation.ChangeDataDescription(id, data);
            Response<string> res = new Response<string>("");
            if (resultValue)
            {
                res.StatusCode = 200;
                res.Message = "Task Description is Updated SuccessFully";
                return Ok(res);
            }
            else
            {
                res.StatusCode = 404;
                res.ErrorMessage = "Task with this id does not found";
                return NotFound(res);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("Error while updating description of the task with id " + id);
            Response<string> res = new Response<string>("");
            res.StatusCode = 500;
            res.ErrorMessage = e.Message;
            return Ok(res);
        }
    }
}