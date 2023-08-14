<script lang="ts">
  import AppHeading from "$lib/components/AppHeading.svelte";
  import { testSettingsStore, testStore } from "$lib/stores";
  import { onMount } from "svelte";
  import { goto } from "$app/navigation";
  import { answerOnTest, putTest } from "$lib/services/test-service";
  import QuizOption from "./QuizOption.svelte";
  import AppButton from "$lib/components/AppButton.svelte";

  let isAnswered = false;

  onMount(async () => {
    if (!$testStore) await goto("/quiz/settings");
  });

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
          <QuizOption {option} on:click={() => chosenOption(option.id)} />
        {/each}
      </div>
      <div class="grid grid-cols-2 gap-6">
        <AppButton on:click={restart}>Restart</AppButton>
        <AppButton on:click={next}>Next</AppButton>
      </div>
    </div>
  </div>
{:else}
  <AppHeading>Loading...</AppHeading>
{/if}
