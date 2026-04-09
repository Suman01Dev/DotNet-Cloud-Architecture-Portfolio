using CloudMonitor.Api.Models;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMonitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContainersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetContainers()
        {
            try
            {
                // Connect to the local Docker socket
                using var client = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();

                // Fetch all containers from your machine
                IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
                    new ContainersListParameters() { All = true });

                // Map the complex Docker data into our clean UI model
                var result = containers.Select(c => new ContainerData
                {
                    Id = c.ID.Substring(0, 12), // Grab the short ID
                    Name = c.Names.FirstOrDefault()?.Replace("/", "") ?? "Unknown", // Clean up the name
                    Image = c.Image,
                    Status = c.State == "running" ? "Running" : "Stopped"
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // If it fails, send a clean error to the frontend
                return StatusCode(500, $"Docker connection error: {ex.Message}");
            }
        }
    }
}