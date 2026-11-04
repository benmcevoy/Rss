export type RouteInfo =
    | { kind: 'home' }
    | { kind: 'folder'; id: string }
    | { kind: 'feed'; id: string }
    | { kind: 'article'; id: string }
    | { kind: 'notFound' }

export function ParseRoute(path: string): RouteInfo {
    const [, section, id] = path.split('/')

    if (!section) return { kind: 'home' }
    
    if (id && (section === 'folder' || section === 'feed' || section === 'article')) {
        return { kind: section, id: decodeURIComponent(id) }
    }
    
    return { kind: 'notFound' }
}