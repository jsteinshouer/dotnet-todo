import { test, expect } from '@playwright/test';

const username = createUsername(8) + '@example.com';
const password = 'P@ssword1';

test.beforeEach(async ({ page }) => {
    //register new user (Could also use request to do this directly with API) or do it on the backend
    await page.goto('/signup');
    await page.getByPlaceholder('Email').fill(username);
    await page.getByPlaceholder('Password', { exact: true }).fill(password);
    await page.getByPlaceholder('Confirm Password').fill(password);
    await page.getByRole('button', { name: 'Signup' }).click();

    //Authenticate new user
    await page.waitForURL('/login');
    await page.getByPlaceholder('Email').fill(username);
    await page.getByPlaceholder('Password').fill(password);
    await page.getByRole('button', { name: 'Login' }).click();

    // Wait for the final URL to ensure that the cookies are actually set.
    await page.waitForURL('/');
    // Alternatively, you can wait until the page reaches a state where all cookies are set.
    await expect(page.getByRole('heading', { name: 'To Do' })).toBeVisible();

    // End of authentication steps.

    await page.goto('/');
});

test('add a todo', async ({ page }) => {
    await page.getByPlaceholder("To Do").fill("Mow the lawn");
    await page.getByRole("button", {"name": "Add"}).click();

    await expect( page.getByRole("listitem")).toHaveText("Mow the lawn");
});


function createUsername(length: number) {
    const randomLetter = () => String.fromCharCode(0 | Math.random() * 26 + 97)

    let username = "";

    let counter = 0;
    while (counter < length) {
        username += randomLetter();
        counter += 1;
    }

    return username;
}