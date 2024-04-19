﻿namespace Infrastructure.Dtos;

public enum StatusCode
{
	OK = 200,
	ERROR = 400,
	EXISTS = 409,
	NOT_FOUND = 404
}

public class ResponseResult
{
	public StatusCode StatusCode { get; set; }
	public object? ContentResult { get; set; }
	public string? Message { get; set; }
}
