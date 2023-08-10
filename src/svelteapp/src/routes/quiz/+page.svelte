<script lang="ts">
  import AppHeading from "$lib/components/AppHeading.svelte";
  import { testSettingsStore, testStore } from "$lib/stores";
  import { onMount } from "svelte";
  import { goto } from "$app/navigation";
  import { answerOnTest, putTest } from "$lib/services/test-service";

  let isAnswered = false;

  onMount(async () => {
    if (!$testStore) await goto("/quiz/settings");
  });

  function buttonStyle(isChosen?: boolean, isCorrect?: boolean) {
    return isChosen
      ? isCorrect
        ? "variant-soft-success"
        : "variant-soft-error"
      : isCorrect
        ? "variant-ghost-success"
        : "variant-ghost-surface";
  }

  async function chosenOption(optionId: number) {
    if (isAnswered) return;
    const test = await answerOnTest($testStore!.id, { optionId });
    testStore.set(test);
    isAnswered = true;
  }

  async function restart() {
    testStore.set(null);
    await goto("/quiz/settings");
  }

  async function next() {
    if (!isAnswered) return;
    isAnswered = false;
    const test = await putTest($testSettingsStore);
    testStore.set(test);
  }

</script>

{#if $testStore}
  <AppHeading>{$testStore.string}</AppHeading>

  <div class="w-1/3 mx-auto">
    <div class="flex flex-col gap-8">

      <div class="grid grid-cols-none gap-4">
        {#each $testStore.options as option (option.id)}
          <button
            type="button"
            class="btn {buttonStyle(option.isChosen, option.isCorrect)}"
            on:click={() => chosenOption(option.id)}
          >
            {option.string}
          </button>
        {/each}
      </div>

      <div class="grid grid-cols-2 gap-6">
        <button type="button" class="btn variant-ghost-surface" on:click={restart}>
          Restart
        </button>
        <button type="button" class="btn variant-ghost-surface" on:click={next}>
          Next
        </button>
      </div>

    </div>
  </div>
{:else}
  <AppHeading>Loading...</AppHeading>
{/if}
