import styles from './rssItems.module.css'
import type { RssItemList } from "../../api/rss"

export function RssItemList({ data }: { data: RssItemList | null }) {
    if (data === null) return <></>;

    return <>
        <div className={styles.rssItems}>
            <h3>{data.name}</h3>
            <ul>
                {data.rssItems?.map(article => {
                    const feed = data.feeds?.find(x=> x.id == article.feedId);
                    return <li key={article.id}>
                        <a className={styles.title} href={`/article/${article.id}`}>{article.name}</a>
                        <div className={styles.meta}>
                            <a href={`/feed/${article.feedId}`}><img className="favIcon" src={feed?.favIcon ?? "/icon-rss.png"} /> {feed?.name}</a>
                            <time dateTime={article.publishedDateTime}>{article.publishedDateTime}</time>
                        </div>
                        <p className={styles.snippet}>{article.snippet}</p>
                    </li>
                })}
            </ul>
        </div>
    </>
}

