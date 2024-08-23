using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Web3Studio.Util
{
    /// <summary>
    /// Monad struct for Json RPC responses. It can have either <see cref="Response"/> or <see cref="Error"/>.
    /// If the <see cref="Response"/> is not null, it means that the request was successful and <see cref="Response"/> contains the result of the call.
    /// If the <see cref="Error"/> is not null, it means that the request failed and <see cref="Error"/> contains the error message.
    /// </summary>
    /// <typeparam name="TResponse">Type of Json RPC result</typeparam>
    public readonly struct JsonRpcResult<TResponse> : IEquatable<JsonRpcResult<TResponse>>
    {
        public TResponse Response { get; }
        public JsonRpcResponseError? Error { get; }

        private JsonRpcResult(TResponse response)
        {
            Response = response;
            Error = null;
        }

        private JsonRpcResult(JsonRpcResponseError error)
        {
            Response = default;
            Error = error;
        }

        public static implicit operator JsonRpcResult<TResponse>(TResponse value) => new JsonRpcResult<TResponse>(value);

        public static implicit operator JsonRpcResult<TResponse>(JsonRpcResponseError error) => new JsonRpcResult<TResponse>(error);

        // [MemberNotNullWhen(true, nameof(Response))]
        // [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess => Error is null;

        // [MemberNotNullWhen(false, nameof(Response))]
        // [MemberNotNullWhen(true, nameof(Error))]
        public bool IsNotSuccess => !IsSuccess;

        /// <summary>
        /// Execute a function based on the result or error of the current monad.
        /// </summary>
        /// <param name="valueAction">Action to execute if the result exists</param>
        /// <param name="errorFunction">Action to execute if the error exists</param>
        public void Execute(Action<TResponse> valueAction, Action<JsonRpcResponseError> errorFunction)
        {
            if (IsSuccess)
                valueAction(Response);
            else
                errorFunction(Error);
        }

        /// <summary>
        /// Return a common result based on the result or error of the current monad.
        /// </summary>
        /// <param name="valueFunction">Function to execute if the result exists</param>
        /// <param name="errorFunction">Function to execute if the error exists</param>
        /// <typeparam name="T">Return type</typeparam>
        /// <returns>A common result</returns>
        public T Match<T>(Func<TResponse, T> valueFunction, Func<JsonRpcResponseError, T> errorFunction)
        {
            if (IsSuccess)
                return valueFunction(Response);
            return errorFunction(Error);
        }

        /// <summary>
        /// Quickly convert the monad to a <see cref="TResponse"/> type object.
        /// It is effectively the same as `result.Value ?? defaultValue`
        /// </summary>
        /// <param name="defaultValue">The value to be returned if the result does not exit</param>
        /// <returns>A valid result object</returns>
        public TResponse OrDefault(TResponse defaultValue = default) => IsSuccess ? Response : defaultValue;

        /// <summary>
        /// Safely obtain the result of the monad.
        /// </summary>
        /// <param name="result">Result object or null</param>
        /// <returns>Whether or not result exists</returns>
        public bool TryGetResult([NotNullWhen(true)] out TResponse result)
        {
            if (IsSuccess)
            {
                result = Response;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Safely obtain the error of the monad.
        /// </summary>
        /// <param name="error">Error object or null</param>
        /// <returns>Whether or not error exists</returns>
        public bool TryGetError([NotNullWhen(true)] out JsonRpcResponseError? error)
        {
            if (IsNotSuccess)
            {
                error = Error;
                return true;
            }

            error = default;
            return false;
        }

        public override string ToString()
        {
            if (IsSuccess)
                return Response.ToString() ?? "null";

            return JsonConvert.SerializeObject(Error);
        }

        public bool Equals(JsonRpcResult<TResponse> other)
        {
            // If their success states are different
            if (IsSuccess != other.IsSuccess)
                return false;

            // If their success states are the same
            if (IsSuccess && other.IsSuccess)
                return EqualityComparer<TResponse>.Default.Equals(Response, other.Response);

            return Error?.Code == other.Error?.Code;
        }

        public override bool Equals(object? other) => other?.GetType() == GetType() && Equals((JsonRpcResult<TResponse>) other);
        public override int GetHashCode() => HashCode.Combine(Response, Error);
        public static bool operator ==(JsonRpcResult<TResponse>? a, JsonRpcResult<TResponse>? b) => a?.Equals(b) ?? false;
        public static bool operator !=(JsonRpcResult<TResponse>? a, JsonRpcResult<TResponse>? b) => !a?.Equals(b) ?? true;
    }
}