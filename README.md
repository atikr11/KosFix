# KosFix

A prototype for reporting local infrastructure problems in Kosovo. In the current demo, a resident selects a municipality and saves a report to that municipality's **Mock Inbox**. Updating a status there also updates **My Reports**. Everything stays in memory and resets when the app process restarts. No report, photo, or status is sent to a municipality.

## Manual smoke test

The repository contains MAUI source files, but not a buildable project file. Run these steps in the original MAUI project with these files included:

1. Open **Home → Report a problem**. Choose a category, leave the municipality blank, and try saving. Expect a validation message and no new report.
2. Choose **Mitrovica e Jugut**, enter a fictional location (e.g. Test Street), and save. Expect a demo report ID, the municipality, and confirmation that nothing was sent.
3. In **Mock Inbox**, select **Pejë**. The new report ID should not appear. Select **Mitrovica e Jugut**; the report should appear with status **Submitted**.
4. Change the status to **In progress** in the inbox. In **My Reports**, find that same ID, including under the **In progress** filter. Open its details and confirm the selected status and municipality.
5. Fully stop and relaunch the app. The new report and status change should be gone; the seeded reports are still present.
