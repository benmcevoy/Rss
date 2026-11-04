import { useEffect, useState } from 'react'
import { ParseRoute, type RouteInfo } from './router'
import {
    type Subscription,
    type RssItemList,
    type RssItem,
    getV1ContentStream, getV1Subscription, getV1ContentFolderById, getV1ContentFeedById, postV1ContentRead
} from "../api/rss"

export type Current =
    | { kind: 'list'; data: RssItemList }
    | { kind: 'item'; data: RssItem }
    | { kind: 'none' }

export type ViewModel = {
    subscriptions: Subscription,
    content: Current
}

export function useLoadPage(location: string): ViewModel {
    const route = ParseRoute(location!);
    const [content, setContent] = useState<Current>({ kind: 'none' })
    const [subscriptions, setSubscriptions] = useState<Subscription | null>(null);

    useEffect(() => { GetSubscriptions().then(res => setSubscriptions(res ?? null)) }, [location]);

    useEffect(() => {
        let ignore = false
        LoadContent(route).then(c => { if (!ignore) setContent(c) })
        return () => { ignore = true }
    }, [location])
    
    return { subscriptions: subscriptions!, content: content }
}

function LoadContent(route: RouteInfo): Promise<Current> {
    switch (route.kind) {
        case 'folder':
            return GetFolder(route.id).then(data => ({ kind: 'list', data }))
        case 'feed':
            return GetFeed(route.id).then(data => ({ kind: 'list', data }))
        case 'article':
            return GetArticle(route.id).then(data => ({ kind: 'item', data }))
        default: // home, notFound
            return GetStream().then(data => ({ kind: 'list', data }))
    }
}

async function GetSubscriptions(): Promise<Subscription> {
    const { data } = await getV1Subscription({
        throwOnError: true,
    })
    return data
}

async function GetStream(): Promise<RssItemList> {
    const { data } = await getV1ContentStream({

        throwOnError: true,
    })
    return data
};

async function GetFolder(id: string): Promise<RssItemList> {
    const { data } = await getV1ContentFolderById({
        path: { id: id },
        throwOnError: true,
    })
    return data
};

async function GetFeed(id: string): Promise<RssItemList> {
    const { data } = await getV1ContentFeedById({
        path: { id: id },
        throwOnError: true,
    })
    return data
};

async function GetArticle(id: string): Promise<RssItem> {
    const { data } = await postV1ContentRead({
        query: { id: id },
        throwOnError: true,
    })
    return data
};