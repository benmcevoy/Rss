import styles from './sidebar.module.css'
import type { Subscription } from "../../api/rss"

export default function Sidebar({ data }: { data: Subscription | null }) {
    if (data == null) return <></>

    return <>
        <nav data-component="sidebar" className={styles.sidebar}>
            <h3>{data?.name}</h3>

            <ul>
                {data?.folders?.map((folder, folderIndex) => (
                    <li key={`folder-${folderIndex}`}>
                        <details open>
                            <summary><a href={`/folder/${folder.id}`}>
                                <span>{folder.name}</span>
                                <span className={styles.count}>{folder.count}</span>
                            </a></summary>
                            <ul>
                                {folder.feeds?.map((feed, feedIndex) => (
                                    <li key={`folder-${folderIndex}-${feedIndex}`}>
                                         <a href={`/feed/${feed.id}`}>
                                            <span><img className="favIcon" src={feed.favIcon ?? "/icon-rss.png"} /> {feed.name}</span>
                                            <span className={styles.count}>{feed.count}</span>
                                        </a>
                                    </li>
                                ))}
                            </ul>
                        </details>
                    </li>
                ))}
            </ul>
        </nav>
    </>
}