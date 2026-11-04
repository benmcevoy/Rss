import styles from './rssItems.module.css'
import type { RssItemList } from "../../api/rss"

export function RssItemList({ data }: { data: RssItemList | null }) {
    if (data === null) return <></>;

    return <>
        <div className={styles.rssItems}>
            <h3>{data.name}</h3>
            <ul>
                {data.rssItems?.map((article) => (
                    <li key={article.id}>
                        <a className={styles.title} href={`/article/${article.id}`}>{article.name}</a>
                        <div className={styles.meta}>
                            <a href={`/feed/${article.feedId}`}>{article.feedName}</a>
                            <time dateTime={article.publishedDateTime}>{article.publishedDateTime}</time>
                        </div>
                        <p className={styles.snippet}>{article.snippet}</p>
                    </li>
                ))}
            </ul>
        </div>
    </>
}

