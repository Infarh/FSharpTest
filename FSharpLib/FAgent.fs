namespace FSharpLib

open System
open System.Collections.Generic

module FAgent = 
    let RegisterUserConnection (id:Guid, userName:string) =
        printf "register id:%s user:%s" (id.ToString()) userName

    let DeregisterUserConnection (id:Guid, userName:string) =
        printf "deregister id:%s user:%s" (id.ToString()) userName

type AgentMessage =
    | AddIfNotExists of id:Guid * userName:string
    | RemoveIfNotExists of id:Guid

type AgentOnlineUsers() =
    let agent = MailboxProcessor<AgentMessage>.Start(fun inbox ->
        let onlineUsers = Dictionary<Guid, string>()
        let rec loop() = async {
            let! msg = inbox.Receive()
            match msg with
            | AddIfNotExists(id, userName) ->
                let exists, _ = onlineUsers.TryGetValue(id)
                if not exists = true then
                    onlineUsers.Add(id, userName)
                    FAgent.RegisterUserConnection(id, userName)
            | RemoveIfNotExists(id) ->
                let exists, userName = onlineUsers.TryGetValue(id)
                if exists = true then
                    onlineUsers.Remove(id) |> ignore
                    FAgent.DeregisterUserConnection(id, userName)
            return! loop() }
        loop())

    member _.AddIfNotExists (id:Guid, userName:string) = 
        printf "AddIfNotExists"

    member _.RemoveIfNotExists (id:Guid) = 
        printf "RemoveIfNotExists"

