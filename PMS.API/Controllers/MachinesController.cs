using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.API.Models;

namespace PMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class MachinesController : Controller
	{
		private readonly PMSDbContext _pmsDbContext;

		public MachinesController(PMSDbContext pmsDbContext)
		{
			this._pmsDbContext = pmsDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllMachines()
		{
			var machines = await _pmsDbContext.Machines.ToListAsync();

			return Ok(machines);
		}

		[HttpPost]
		public async Task<IActionResult> AddMachine([FromBody] Machine machine)
		{
			machine.Id = Guid.NewGuid();

			await _pmsDbContext.Machines.AddAsync(machine);
			await _pmsDbContext.SaveChangesAsync();

			return Ok(machine);
		}

		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetMachine(Guid id)
		{
			var machine = await _pmsDbContext.Machines.FirstOrDefaultAsync(x => x.Id == id);

			if(machine == null)
				return NotFound();
			return Ok(machine);
		}

		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> UpdateMachine(Guid id, [FromBody] Machine updatedMachine)
		{
			var machine = await _pmsDbContext.Machines.FindAsync(id);

			if (machine == null)
				return NotFound();

			machine.Name = updatedMachine.Name;

			await _pmsDbContext.SaveChangesAsync();

			return Ok(machine);
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> DeleteMachine(Guid id)
		{
			var machine = await _pmsDbContext.Machines.FindAsync(id);

			if(machine == null)
				return NotFound();

			_pmsDbContext.Machines.Remove(machine);
			await _pmsDbContext.SaveChangesAsync();

			return Ok(machine);
		}

	}
}
