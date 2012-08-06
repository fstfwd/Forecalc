﻿namespace Forecalc.Library

module QuadTree =

    let private logw = 4
    let private w = 1 <<< logw
    let private mw = w - 1
    let private sizew = 1 <<< (4 * logw)
    let private logh = 5
    let private h = 1 <<< logh
    let private mh = h - 1
    let private sizeh = 1 <<< (4 * logh)

    let private validate (c, r) =
        if c < 0 || c >= sizew then
            failwithf "c must be between 0 and %i" (sizew - 1)
        if r < 0 || r >= sizeh then
            failwithf "r must be between 0 and %i" (sizeh - 1)

    type quadtree<'a> internal() =
        let t0 = Array.zeroCreate<'a option [] option [] option [] option> (w * h)
        with 
            member internal this.Tile0 
                with get() = t0
            member this.Item
                with get(c, r) =
                    validate (c, r)
                    let v = t0.[(((c >>> (3 * logw)) &&& mw) <<< logh) + ((r >>> (3 * logh)) &&& mh)]
                    match v with
                        | None -> None
                        | Some(t1) ->
                            let v = t1.[(((c >>> (2 * logw)) &&& mw) <<< logh) + ((r >>> (2 * logh)) &&& mh)]
                            match v with
                                | None -> None
                                | Some(t2) ->
                                    let v = t2.[(((c >>> (1 * logw)) &&& mw) <<< logh) + ((r >>> (1 * logh)) &&& mh)]
                                    match v with
                                        | None -> None
                                        | Some(t3) -> t3.[((c &&& mw) <<< logh) + (r &&& mh)]
                and set(c, r) (v : 'a option) =
                    validate (c, r)
                    let i0 = (((c >>> (3 * logw)) &&& mw) <<< logh) + ((r >>> (3 * logh)) &&& mh)
                    let t1() = t0.[i0]
                    if t1().IsNone && v.IsSome then
                        t0.[i0] <- Some(Array.zeroCreate<'a option [] option [] option> (w * h))
                    if t1().IsSome then
                        let i1 = (((c >>> (2 * logw)) &&& mw) <<< logh) + ((r >>> (2 * logh)) &&& mh)
                        let t2() = t1().Value.[i1]
                        if t2().IsNone && v.IsSome then
                            t1().Value.[i1] <- Some(Array.zeroCreate<'a option [] option> (w * h))
                        if t2().IsSome then
                            let i2 = (((c >>> (1 * logw)) &&& mw) <<< logh) + ((r >>> (1 * logh)) &&& mh)                         
                            let t3() = t2().Value.[i2]
                            if t3().IsNone && v.IsSome then
                                t2().Value.[i2] <- Some(Array.zeroCreate<'a option> (w * h))
                            if t3().IsSome then
                                let i3 = ((c &&& mw) <<< logh) + (r &&& mh)
                                t3().Value.[i3] <- v
            member this.AsEnumerable() =
                seq {
                    for l in t0 do
                        match l with
                            | None -> ()
                            | Some(v) ->
                                for l in v do
                                    match l with
                                        | None -> ()
                                        | Some(v) ->
                                            for l in v do
                                                match l with
                                                    | None -> ()
                                                    | Some(v) ->
                                                        for l in v do
                                                            match l with
                                                                | None -> ()
                                                                | Some(v) -> yield v
                }
            member this.Length
                with get() =
                    this.AsEnumerable() |> Seq.length
            member this.IsEmpty
                with get() =
                    this.Length = 0

    let create<'a>() = quadtree<'a>()

    let get (c, r) (quadtree : quadtree<'a>) =
        quadtree.[c, r]
        
    let set (c, r) v (quadtree : quadtree<'a>) =
        quadtree.[c, r] <- v

    let isEmpty (quadtree : quadtree<'a>) =
        quadtree.IsEmpty

    let length (quadtree : quadtree<'a>) =
        quadtree.Length

    let apply f (quadtree : quadtree<'a>) =
        quadtree.Tile0 |> Array.iteri (fun i0 t0 ->
            match t0 with
                | None -> ()
                | Some(v0) ->
                    v0 |> Array.iteri (fun i1 t1 ->
                        match t1 with
                            | None -> ()
                            | Some(v1) ->
                                v1 |> Array.iteri (fun i2 t2 ->
                                    match t2 with
                                        | None -> ()
                                        | Some(v2) ->
                                            v2 |> Array.iteri (fun i3 t3 ->
                                                match t3 with
                                                    | Some(v) -> quadtree.Tile0.[i0].Value.[i1].Value.[i2].Value.[i3] <- Some(f v)
                                                    | None -> ()))))

    let filter f (source : quadtree<'a>) =
        let l = w * h - 1
        let target = quadtree<'a>()
        for i0 in [ 0 .. l ] do
            let v1 = source.Tile0.[i0]
            let c0 = (i0 >>> logh) <<< (3 * logw)
            let r0 = (i0 &&& mh) <<< (3 * logh)
            match v1 with
                | None -> ()
                | Some(t1) ->
                    for i1 in [ 0 .. l ] do
                        let v2 = t1.[i1]
                        let c1 = (i1 >>> logh) <<< (2 * logw)
                        let r1 = (i1 &&& mh) <<< (2 * logh)
                        match v2 with
                            | None -> ()
                            | Some(t2) ->
                                for i2 in [ 0 .. l ] do
                                    let v3 = t2.[i2]
                                    let c2 = (i2 >>> logh) <<< logw
                                    let r2 = (i2 &&& mh) <<< logh
                                    match v3 with
                                        | None -> ()
                                        | Some(t3) ->
                                            for i3 in [ 0 .. l ] do
                                                let v = t3.[i3]
                                                match v with
                                                    | None -> ()
                                                    | Some(t) ->
                                                        let c = c0 ||| c1 ||| c2 ||| (i3 >>> logh)
                                                        let r = r0 ||| r1 ||| r2 ||| (i3 &&& mh)
                                                        if f t then
                                                            target.[c, r] <- Some(t)
        target

    let iter f (quadtree : quadtree<'a>) =
        for l in quadtree.Tile0 do
                match l with
                    | None -> ()
                    | Some(v) ->
                        for l in v do
                            match l with
                                | None -> ()
                                | Some(v) ->
                                    for l in v do
                                        match l with
                                            | None -> ()
                                            | Some(v) ->
                                                for l in v do
                                                    match l with
                                                        | None -> ()
                                                        | Some(v) -> f v
                                                                      
    let iteri f (quadtree : quadtree<'a>) =
        let l = w * h - 1
        for i0 in [ 0 .. l ] do
            let v1 = quadtree.Tile0.[i0]
            let c0 = (i0 >>> logh) <<< (3 * logw)
            let r0 = (i0 &&& mh) <<< (3 * logh)
            match v1 with
                | None -> ()
                | Some(t1) ->
                    for i1 in [ 0 .. l ] do
                        let v2 = t1.[i1]
                        let c1 = (i1 >>> logh) <<< (2 * logw)
                        let r1 = (i1 &&& mh) <<< (2 * logh)
                        match v2 with
                            | None -> ()
                            | Some(t2) ->
                                for i2 in [ 0 .. l ] do
                                    let v3 = t2.[i2]
                                    let c2 = (i2 >>> logh) <<< logw
                                    let r2 = (i2 &&& mh) <<< logh
                                    match v3 with
                                        | None -> ()
                                        | Some(t3) ->
                                            for i3 in [ 0 .. l ] do
                                                let v = t3.[i3]
                                                match v with
                                                    | None -> ()
                                                    | Some(t) ->
                                                        let c = c0 ||| c1 ||| c2 ||| (i3 >>> logh)
                                                        let r = r0 ||| r1 ||| r2 ||| (i3 &&& mh)
                                                        f c r t

                                                        
    let map f (source : quadtree<'a>) =
        let l = w * h - 1
        let target = quadtree<'b>()
        for i0 in [ 0 .. l ] do
            let v1 = source.Tile0.[i0]
            let c0 = (i0 >>> logh) <<< (3 * logw)
            let r0 = (i0 &&& mh) <<< (3 * logh)
            match v1 with
                | None -> ()
                | Some(t1) ->
                    for i1 in [ 0 .. l ] do
                        let v2 = t1.[i1]
                        let c1 = (i1 >>> logh) <<< (2 * logw)
                        let r1 = (i1 &&& mh) <<< (2 * logh)
                        match v2 with
                            | None -> ()
                            | Some(t2) ->
                                for i2 in [ 0 .. l ] do
                                    let v3 = t2.[i2]
                                    let c2 = (i2 >>> logh) <<< logw
                                    let r2 = (i2 &&& mh) <<< logh
                                    match v3 with
                                        | None -> ()
                                        | Some(t3) ->
                                            for i3 in [ 0 .. l ] do
                                                let v = t3.[i3]
                                                match v with
                                                    | None -> ()
                                                    | Some(t) ->
                                                        let c = c0 ||| c1 ||| c2 ||| (i3 >>> logh)
                                                        let r = r0 ||| r1 ||| r2 ||| (i3 &&& mh)
                                                        target.[c, r] <- Some(f t)
        target
       
    let mapi f (source : quadtree<'a>) =
        let l = w * h - 1
        let target = quadtree<'b>()
        for i0 in [ 0 .. l ] do
            let v1 = source.Tile0.[i0]
            let c0 = (i0 >>> logh) <<< (3 * logw)
            let r0 = (i0 &&& mh) <<< (3 * logh)
            match v1 with
                | None -> ()
                | Some(t1) ->
                    for i1 in [ 0 .. l ] do
                        let v2 = t1.[i1]
                        let c1 = (i1 >>> logh) <<< (2 * logw)
                        let r1 = (i1 &&& mh) <<< (2 * logh)
                        match v2 with
                            | None -> ()
                            | Some(t2) ->
                                for i2 in [ 0 .. l ] do
                                    let v3 = t2.[i2]
                                    let c2 = (i2 >>> logh) <<< logw
                                    let r2 = (i2 &&& mh) <<< logh
                                    match v3 with
                                        | None -> ()
                                        | Some(t3) ->
                                            for i3 in [ 0 .. l ] do
                                                let v = t3.[i3]
                                                match v with
                                                    | None -> ()
                                                    | Some(t) ->
                                                        let c = c0 ||| c1 ||| c2 ||| (i3 >>> logh)
                                                        let r = r0 ||| r1 ||| r2 ||| (i3 &&& mh)
                                                        target.[c, r] <- Some(f c r t)
        target
                                        
    let toSeq (quadtree : quadtree<'a>) =
        quadtree.AsEnumerable()