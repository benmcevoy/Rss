import styles from './global-navigation.module.css'

export default function GlobalNavigation() {
    return <>
        <div className={styles.globalNavigation}>
            <a href="/">Home</a>
        </div>
    </>
}